using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DataEditor.Help
{
    public class AnotherTextManager
    {
        // #{0:d3}#:#{0}#,|id,|name
        // #{0:d3}#:#{0}#,Skills|#|id,Skills|#|name
        List<Factor> factors = null;
        public AnotherTextManager()
        {   }
        List<string> FormatTemp = new List<string>();
        protected char KeyChar = '\0';
        public bool Initialize(string format, params object[] data)
        {
            string[] parts = format.Split(',');
            // 第一个是完全的字符模板
            string OriginFormat = parts[0];
            // 拆解之
            FormatTemp.Clear();
            Regex reg = new Regex("#({(.+?)}){0,1}#");
            string Temp = reg.Replace(OriginFormat, MatchEvaluator);
            string[] partial = Temp.Split(KeyChar);
            // 从第二个起，依次配置
            factors = new List<Factor>();
            for (int i = 0, j = 0; i < partial.Length && j < parts.Length - 1; i++)
            {
                if (partial[i] == "")
                {
                    factors.Add(ContentFactor.Allocate(FormatTemp[j], parts[j + 1], data));
                    j++;
                }
                else
                    factors.Add(new StringFactor(partial[i]));
            }
            if ( parts.Length == 1 ) factors.Add(new StringFactor(parts[0]));
            return true;
        }
        protected string MatchEvaluator(Match match)
        {
            CaptureCollection collection = match.Groups[2].Captures;
            FormatTemp.Add(collection.Count == 0 ? "" : "{" + collection[0].Value + "}");
            return KeyChar.ToString();
        }
        public override string ToString()
        {
            if (factors == null) return "";
            StringBuilder sb = new StringBuilder();
            foreach (Factor f in factors) sb.Append(f.GetString());
            return sb.ToString();
        }
        abstract class Factor
        {
            public abstract string GetString();
        }
        class StringFactor : Factor
        {
            string text;
            public StringFactor(string text) { this.text = text; }
            public override string GetString() { return text; }
        }
        class ContentFactor : Factor
        {
            object[] List;
            object Root;
            string Format;
            public ContentFactor(object root, object[] list, string format) 
            { List = list; Root = root; Format = format; }
            public override string GetString()
            {
                object ob = PathHelper.LoadByArray(Root, List);
                if (ob is FuzzyData.FuzzyFixnum) ob = (ob as FuzzyData.FuzzyFixnum).Value;
                else if (ob is FuzzyData.FuzzyString) ob = (ob as FuzzyData.FuzzyString).Text;
                return string.Format(Format, ob);
            }
            static public ContentFactor Allocate(string format, string path, params object[] para)
            {
                // 拆解各个参数部分
                string[] keys = path.Split('|');
                int max = para.Length, now = 0;
                // 计算根值 TODO
                object root;
                Regex reg = new Regex("\\[.+?\\]");
                Match m;
                if ( keys[0] == "" || keys[0] == "#" )
                    root = para[0];
                else if ( (m = reg.Match(keys[0])).Success )
                {
                    string all = m.Value;
                    all = all.Remove(0, 1);
                    all = all.Remove(all.Length - 1, 1);
                    string[] parts = all.Split('/');
                    FuzzyData.FuzzyArray fa = new FuzzyData.FuzzyArray(parts);
                    root = fa;
                }
                else
                    root = FileManager.Create(keys[0]).Data;
                keys[0] = null;
                // 结算路径 
                List<object> list = new List<object>();
                foreach (string key in keys)
                    if (key == null) continue;
                    else if ( key == "#")
                        list.Add(para[now]);
                    else if ( key == "##" )
                    {
                        if ( ++now >= max ) throw new ArgumentException("字符串匹配中超界");
                    }
                    else
                        list.Add(key);
                return new ContentFactor(root, list.ToArray(), format);
            }
        }
    }
}
