using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Adapter
{
    /// <summary>
    /// AdvanceArray 要求：所有的部件是相同名字的FuzzyObject
    /// 或者是 FuzzyNil
    /// 否则，生成失败。
    /// </summary>
    public class AdvanceArray : FuzzyData.FuzzyArray,AdvanceData
    {
        /// <summary>
        /// 本 Advance Array 是否可用的标记
        /// </summary>
        public new bool Exists { get { return exists; } }
        /// <summary>
        /// 获取内部类型的名字
        /// </summary>
        public new string ClassName { get { return classname; } }
        /// <summary>
        /// 获取链接表
        /// </summary>
        public Help.LinkTable<int, int> Link { get { return link; } }
        /// <summary>
        /// 获取数据
        /// </summary>
        public List<FuzzyData.FuzzyObject> Data { get { return data; } }

        protected bool exists;
        protected string classname;
        protected Help.LinkTable<int, int> link = new Help.LinkTable<int, int>();
        protected List<FuzzyData.FuzzyObject> data = new List<FuzzyData.FuzzyObject>();

        public AdvanceArray (FuzzyData.FuzzyArray array)
        {
            // Initialize
            exists = false;
            classname = "";
            // Check
            FuzzyData.FuzzyObject fob;
            foreach ( object ob in array )
                if ( (fob = ob as FuzzyData.FuzzyObject) == null )
                    return;
                else
                    if ( fob == FuzzyData.FuzzyNil.Instance )
                        continue;
                    else if ( ClassName == "" )
                        classname = fob.ClassName.Name;
                    else if ( fob.ClassName.Name != ClassName )
                        return;
            exists = true;
            // Data
            int i = 0, j = 0;
            foreach ( object ob in array )
            {
                base.Add(ob);
                fob = ob as FuzzyData.FuzzyObject;
                if ( fob == FuzzyData.FuzzyNil.Instance ) continue;
                else { Link.Add(i++, j); Data.Add(fob); }
                j++;
            }
        }
        public void Refresh ()
        {
            int i = 0, j = 0;
            FuzzyData.FuzzyObject fob;
            link.Clear();
            Data.Clear();
            foreach ( object ob in this )
            {
                fob = ob as FuzzyData.FuzzyObject;
                if ( fob == FuzzyData.FuzzyNil.Instance ) continue;
                else { Link.Add(i++, j); Data.Add(fob); }
                j++;
            }
        }
    }
}