using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Adapter
{/*
    /// <summary>
    /// AdvanceArray 要求：所有的部件是相同名字的FuzzyObject
    /// 或者是 FuzzyNil
    /// 否则，生成失败。
    /// </summary>
    public class AdvanceArray
    {
        protected string className;
        protected FuzzyData.FuzzyArray value;
        protected Help.LinkTable<int, int> link;
        /// <summary>
        /// 获取类型的一并设置名
        /// </summary>
        public string ClassName { get { return className; } }
        /// <summary>
        /// 获取值
        /// </summary>
        public FuzzyData.FuzzyArray Value { get { return value; } }
        /// <summary>
        /// 获取链接表
        /// </summary>
        public Help.LinkTable<int, int> Link { get { return link; } }
        
        protected AdvanceArray() { }
        protected AdvanceArray(FuzzyData.FuzzyArray value) { this.value = value; }

        static public AdvanceArray Get(FuzzyData.FuzzyArray Array)
        {
            AdvanceArray array = new AdvanceArray(Array);
            Help.LinkTable<int, int> Link = new Help.LinkTable<int, int>();
            int i = -1, j = 0;
            foreach (object ob in Array)
            {
                if (ob is FuzzyData.FuzzyNil) i++;
                else if (ob is FuzzyData.FuzzyObject)
                {
                    Link.Add(++i, j++);
                    if (array.className == null)
                        array.className = (ob as FuzzyData.FuzzyObject).ClassName.Name;
                    else if ((ob as FuzzyData.FuzzyObject).ClassName.Name != array.className)
                        return null;
                }
                else return null;
            }
            array.link = Link;
            return array;
        }
    }

    public class AdvanceViewArray : AdvanceArray
    {
        protected List<Help.TextManager[]> list = new List<Help.TextManager[]>();
        protected string[] models;
        public string[] GetString(FuzzyData.FuzzyObject Target)
        {
            for (int i = 0; i < Value.Count; i++)
                if (Value[i] == Target)
                    return GetString(i);
            return null;
        }
        public string[] GetString(int index)
        {
            string[] texts = new string[models.Length];
            Help.TextManager[] managers = list[index];
            for (int i = 0; i < models.Length; i++)
                texts[i] = managers[i].GetString();
            return texts;
        }
        public void Initialize(System.Xml.XmlNode Node)
        {
            List<string> models = new List<string>();
            foreach (System.Xml.XmlNode child in Node.ChildNodes)
                if (child.Name.ToUpper() == "TEXT")
                    models.Add(child.InnerText);
            this.models = models.ToArray();
        }
        public void Bound()
        {
            if (models != null)
                foreach (FuzzyData.FuzzyObject obj in value)
                    Bound(obj);
        }
        protected void Bound(FuzzyData.FuzzyObject obj, int pos = -1)
        {
            if (obj == FuzzyData.FuzzyNil.Instance) return;
            if (pos == -1)
            {
                list.Add(new Help.TextManager[models.Length]);
                pos = list.Count - 1;
            }
            for (int j = 0; j < models.Length; j++)
                list[pos][j] = new Help.TextManager(models[j], obj);
        }
    }
  */
}