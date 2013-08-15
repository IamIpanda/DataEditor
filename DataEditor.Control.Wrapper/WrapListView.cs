using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace DataEditor.Control.Wrapper
{
    public class WrapListView : Prototype.ProtoListView
    {

    }
    public class WrapListViewArgs : ControlArgs
    {
        /// <summary>
        /// 描述列标的信息
        /// </summary>
        public List<WrapListViewRowArgs> Rows { get; set; }
        /// <summary>
        /// 描述当新建的时候，创建的一个新的结构体。
        /// 没有这个结构体的场合，不能进行新建操作。
        /// </summary>
        public FuzzyData.FuzzyObject New { get; set; }
        /// <summary>
        /// 描述当修改的时候，弹出的对话框。
        /// 没有这个结构的场合，不能进行修改操作。
        /// </summary>
        public XmlNode Dialog { get; set; }
        public WrapListViewArgs () : base() 
        {
            Rows = new List<WrapListViewRowArgs>();
            New = FuzzyData.FuzzyNil.Instance;
            Dialog = null;
        }
        public WrapListViewArgs (XmlNode Node) : base(Node) { }
        public override void Load (System.Xml.XmlNode Node)
        {
            base.Load(Node);
            string Name;
            foreach ( XmlNode child in Node.ChildNodes )
            {
                Name = child.Name.ToUpper();
                if ( Name == "ROW" )
                    Rows.Add(new WrapListViewRowArgs(child));
                else if ( Name == "NEW" )
                {
                    Contract.Serialization xml = Help.SerializationManager.TryGetSerialization("[x]");
                    if ( xml == null ) continue;
                    try
                    {
                        Type type = xml.GetType();
                        System.Reflection.MethodInfo method = type.GetMethod("Load");
                        object ob = method.Invoke(xml, new object[] { child.FirstChild });
                        New = ob as FuzzyData.FuzzyObject;
                    }
                    catch ( Exception ex )
                    { Help.Log.log("ListView：载入 New 节点失败：" + ex.Message); }
                }
                else if ( Name == "DIALOG" )
                    Dialog = child;
                else
                    OnScan(Name, child.InnerText);
            }

        }
        /// <summary>
        /// 节点的名称为 Row
        /// 节点的 Text 子节点作为 Format 字段
        /// 节点的 Actual 子节点作为 Actual 字段
        /// </summary>
        public class WrapListViewRowArgs : ControlArgs
        {
            public WrapListViewRowArgs () : base() { }
            public WrapListViewRowArgs (XmlNode Node) : base(Node) { }
        }
    }
} 