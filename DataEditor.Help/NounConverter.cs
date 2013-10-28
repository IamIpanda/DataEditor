using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace DataEditor.Help
{
    public static class NounConverter
    {
        static Dictionary<string, string> Cons = new Dictionary<string, string>();
        static NounConverter ()
        {
        }
        static public void Load (XmlNode Node)
        {
            if ( Node.Name.ToUpper() != "CONVERTS" )
            {
                Log.log("NounConverter : 不正确的节点名称：" + Node.Name);
                return;
            }
            foreach ( XmlNode child in Node.ChildNodes )
                if ( Cons.ContainsKey(child.Name) )
                {
                    Log.log("NounConverter : 修正了节点 " + child.Name + "[" + Cons[child.Name] + "] => ["
                        + child.InnerText + "]");
                    Cons[child.Name] = child.Value;
                }
                else
                    Cons.Add(child.Name, child.InnerText);
        }
        static public void Load (string name)
        {
            if ( System.IO.File.Exists(name) == false ) { Log.log("NounConverter : 文件不存在：" + name); return; }
            XmlDocument document = new XmlDocument();
            try { document.Load(name); }
            catch { Log.log("NounConverter : 不正确的文件：" + name); return; }
            XmlNode Node = document.FirstChild;
            while ( Node.NextSibling != null && Node.Name.ToUpper() != "CONVERTS" )
                Node = Node.NextSibling;
            if ( Node == null ) { Log.log("NounConverter : 找不到 CONVERTS 节点：" + name); }
            else Load(Node);
        }
        static public void Clear ()
        {
            Cons.Clear();
        }
        static public void Add (string s1, string s2) { Cons.Add(s1, s2); }
        static public void Add (XmlNode node) { Add(node.Name, node.InnerText); }
        static public string Get (string Key)
        {
            string ans = null;
            Cons.TryGetValue(Key, out ans);
            return ans ?? Key;
        }
    }
}
