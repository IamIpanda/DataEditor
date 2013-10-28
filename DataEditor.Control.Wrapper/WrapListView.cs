using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace DataEditor.Control.Wrapper
{

    public class WrapListView : Control.WrapBaseEditor<Adapter.AdvanceArray,ListViewArgs>
    {
        List<ListViewArgs.ListViewColumnArgs> ColumnArgs;
        List<Help.AnotherTextManager[]> TextTable = new List<Help.AnotherTextManager[]>();
        FuzzyData.FuzzyObject New = null;
        XmlNode Window = null;
        Prototype.ProtoListView plv;
        public override string Flag { get { return "rows"; } }
        public override void Bind () 
        {
            plv = new Prototype.ProtoListView();
            plv.DoubleClick += this.WrapListView_DoubleClick;
            Binding = plv;
        }
        public override  void Push ()
        {
            if ( value != null )
                value.Refresh();
        }
        public override void Reset ()
        {
            base.Reset();
            if ( argument == null ) return;
            ColumnArgs = argument.Columns;
            foreach ( var row in argument.Columns )
            {
                int w = row.Width == -1 ? 200 : row.Width;
                plv.Columns.Add(row.Text, w);
            }
            this.New = argument.New;
            this.Window = argument.Dialog;
        }
        public override void Pull ()
        {
            if ( value != null ) ShowText();
        }
        protected override Adapter.AdvanceArray ConvertToValue (FuzzyData.FuzzyObject origin)
        {
            FuzzyData.FuzzyArray arr = origin as FuzzyData.FuzzyArray;
            if ( arr == null ) return null;
            Adapter.AdvanceArray array = new Adapter.AdvanceArray(arr);
            return array.Exists ? array : null;
        }
        void WrapListView_DoubleClick (object sender, EventArgs e)
        {
            if ( value == null ) return;
            if ( plv.SelectedIndices.Count == 0 ) return;
            if ( Window == null ) return;
            FuzzyData.FuzzyObject editing = value.Data[plv.SelectedIndices[0]];
            DataEditor.Control.Window.DataEditorDialog dialog = new Control.Window.DataEditorDialog();
            dialog.Arguments = dialog.Load_Information(Window);
            dialog.Parent = editing;
            if ( (dialog.Binding as System.Windows.Forms.Form).ShowDialog() == DialogResult.OK )
            {
                // TODO : Refresh Text.
            }
        }
        public void ShowText ()
        {
            TextTable.Clear();
            int count = ColumnArgs.Count;
            object temp = null;
            foreach ( FuzzyData.FuzzyObject ob in value.Data )
            {
                Help.AnotherTextManager[] ts = new Help.AnotherTextManager[count];
                for ( int i = 0; i < count; i++ )
                {
                    temp = null;
                    ts[i] = new Help.AnotherTextManager();
                    ob.InstanceVariables.TryGetValue(ColumnArgs[i].Actual, out temp);
                    if ( temp != null ) ts[i].Initialize(ColumnArgs[i].Format, temp);
                }
                TextTable.Add(ts);
            }
            RefreshText();
        }
        public void RefreshText ()
        {
            plv.Items.Clear();
            foreach ( Help.AnotherTextManager[] managers in TextTable )
            {
                string[] ss = new string[managers.Length];
                for ( int i = 0; i < managers.Length; i++ )
                    ss[i] = managers[i].ToString();
                plv.Items.Add(new ListViewItem(ss));
            }
        }
    }
    public class ListViewArgs : ControlArgs
    {
        /// <summary>
        /// 描述列标的信息
        /// </summary>
        public virtual List<ListViewColumnArgs> Columns { get { return columns; } set { columns = value; } }
        List<ListViewColumnArgs> columns = new List<ListViewColumnArgs>();
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
            New = FuzzyData.FuzzyNil.Instance;
            Dialog = null;
        }
        public ListViewArgs (XmlNode Node) : this() { Load(Node); }
        public override void Load (System.Xml.XmlNode Node)
        {
            base.Load(Node);
            string Name;
            foreach ( XmlNode child in Node.ChildNodes )
            {
                Name = child.Name.ToUpper();
                if ( Name == "COLUMN" )
                    Columns.Add(new ListViewColumnArgs(child));
                else if ( Name == "NEW" )
                    New = LoadXmlData(child);
                else if ( Name == "DIALOG" )
                    Dialog = child;
                else
                    OnScan(Name, child.InnerText);
            }
        }
        protected void OriginLoad (System.Xml.XmlNode Node) { base.Load(Node); }
        protected FuzzyData.FuzzyObject LoadXmlData (System.Xml.XmlNode Node)
        {
            Contract.Serialization xml = Help.SerializationManager.TryGetSerialization("[x]");
            if ( xml == null ) return null;
            try
            {
                Type type = xml.GetType();
                System.Reflection.MethodInfo method = type.GetMethod("Load", new Type[] { typeof(XmlNode) });
                object ob = method.Invoke(xml, new object[] { Node.FirstChild });
                return ob as FuzzyData.FuzzyObject;
            }
            catch ( Exception ex )
            { Help.Log.log("ListView：载入 New 节点失败：" + ex.Message); }
            return null;
        }
        /// <summary>
        /// 节点的名称为 Row
        /// 节点的 Text 子节点作为 Text 字段
        /// 节点的 Actual 子节点作为 Actual 字段
        /// 节点的 Width 子节点作为 Width 字段
        /// 节点的 InnerText 作为 Format 字段
        /// </summary>
        public class ListViewColumnArgs : ControlArgs
        {
            public virtual string Format { get; set; }
            public ListViewColumnArgs () : base() { Format = ""; }
            public ListViewColumnArgs (XmlNode Node) : this() { Load(Node); }
            protected override void OnScan (string Name, string InnerText)
            {
                if ( Name == "INNERTEXT" )
                    Format = InnerText;
                base.OnScan(Name, InnerText);
            }
        }
    }
} 