using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace DataEditor.Control.Wrapper
{
    public class WrapListView : Prototype.ProtoListView, Control.ObjectEditor
    {
        ListViewArgs argument;
        Adapter.AdvanceViewArray value;
        FuzzyData.FuzzySymbol key;
        Contract.TaintState taint;
        public ControlArgs Load_Information (System.Xml.XmlNode Node)
        {
            return new ListViewArgs(Node);
        }
        public string Flag { get { return "rows"; } }
        public Label Label { get; set; }
        public event EventHandler Taint;
        public ControlArgs Arguments
        {
            get { return argument; }
            set { argument = value as ListViewArgs; Reset(); }
        }
        public FuzzyData.FuzzyObject Value
        {
            get { return value; }
            set { this.value = value as FuzzyData.FuzzyString; Pull(); }
        }
        public Contract.TaintState Tainted
        {
            get { return taint; }
            set { taint = value; Putt(); }
        }
        public new FuzzyData.FuzzyObject Parent
        {
            set
            {
                //FuzzyData.FuzzyString num = ControlHelper.TypeCheck<FuzzyData.FuzzyString>.Get(value, key);
                //if ( num != null ) Value = num;
                Pull();
            }
        }
        public void Push ()
        {
            if ( value != null )
                value.Text = this.Text;
        }
        public void Reset ()
        {
            if ( argument == null ) return;
            DataEditor.Control.ControlHelper.Reset(this, argument);
            this.key = argument.Actual;
        }
        public void Pull ()
        {
            if ( value != null )
            {
                this.Text = value.Text;
                Tainted = Help.TaintRecord.Single[value];
            }
        }
        public void Putt ()
        {
            Help.TaintHelper.OnPutt(Label, taint);
        }
        public WrapListView ()
        {
            base.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            base.View = System.Windows.Forms.View.Details;
        }
    }
    public class ListViewArgs : ControlArgs
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
        public ListViewArgs () : base() 
        {
            Rows = new List<WrapListViewRowArgs>();
            New = FuzzyData.FuzzyNil.Instance;
            Dialog = null;
        }
        public ListViewArgs (XmlNode Node) : base(Node) { }
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
                        System.Reflection.MethodInfo method = type.GetMethod("Load", new Type[] { typeof(XmlNode) });
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