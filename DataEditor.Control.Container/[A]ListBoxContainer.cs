using System;
using System.Collections.Generic;
using System.Text;
using DataEditor.FuzzyData;
using System.Windows.Forms;

namespace DataEditor.Control.Container
{
    public class ListBoxContainer : Prototype.ProtoFullListBox, Control.ObjectContainer,Control.TaintableMultiEditor 
    {
        ListBoxArgs argument;
        string model;
        Help.LinkTable<int, int> Link = new Help.LinkTable<int, int>();
        List<Help.TextManager> TextManager = new List<Help.TextManager>();
        Help.TaintIndexCollection taint;

        public string Flag { get { return "list"; } }
        public Label Label { get; set; }
        public FuzzyArray value;
        /// <summary>
        /// 此属性的 set 方法已停用。
        /// </summary>
        public FuzzyData.FuzzyObject Value
        {
            get
            {
                if (SelectedIndex == -1) return null;
                if (value == null) return null;
                else return value[Link.Reverse[SelectedIndex]] as FuzzyObject;
            }
            set { throw new NotImplementedException(); }
        }
        
        public new FuzzyData.FuzzyObject Parent
        {
            get { return value; }
            set 
            {
                if (value is FuzzyArray)
                    this.value = value as FuzzyArray;
                Pull();
            }
        }
        public ControlArgs Arguments
        {
            get { return argument; }
            set { argument = value as ListBoxArgs; Reset(); }
        }
        public Contract.TaintCollection Tainted
        {
            get { return taint; }
        }
        public ListBoxContainer()
        {
            InitializeComponent();
        }
        public ControlArgs Load_Information(System.Xml.XmlNode Node)
        {
            DataEditor.Arce.Interpreter.Builder builder = new Arce.Interpreter.Builder();
            System.Drawing.Size size = builder.Build(Node, this.Controls, 10, 10);
            this.SetClientSizeCore(size.Width, size.Height);
            ListBoxArgs gba = new ListBoxArgs();
            gba.Label = 0;
            gba.Load(Node);
            return gba;
        }
        public void Pull()
        {
            if (value == null) return;
            if (model == null) return;
            Link.Clear();
            TextManager.Clear();
            Items.Clear();
            int i = -1, j = 0;
            foreach (object Child in value)
            {
                i++;
                if (Child == null || Child is FuzzyNil) continue;
                FuzzyObject child = Child as FuzzyObject;
                if (child == null) continue;
                Help.TextManager text = new Help.TextManager(model, child);
                text.TextChanged += TextManager_TextChanged;
                Items.Add(text);
                Link.Add(i, j++);
            }
            if (Help.TaintRecord.Multi[value] == null || !(Help.TaintRecord.Multi[value] is Help.TaintIndexCollection))
                Help.TaintRecord.Multi[value] = new Help.TaintIndexCollection(1);
            taint = Help.TaintRecord.Multi[value] as Help.TaintIndexCollection;
            Putt();
        }
        public void Push() { }
        public void Reset()
        {
            if (argument == null) return;
            ControlHelper.Reset(this, argument);
            if (argument.BackColor != default(System.Drawing.Color))
                this.BackColor = argument.BackColor;
            if (argument.Dock != -1)
                this.Dock = (DockStyle)argument.Dock;
            this.Text = argument.Text;
            this.model = argument.Show;
        }
        public void PushChild()
        {
            if (Value == null) return;
            foreach(System.Windows.Forms.Control control in Controls)
            {
                if (control is ObjectEditor)
                    (control as ObjectEditor).Parent = Value;
            }
        }
        // TODO : Finish this.
        public void Putt()
        {
            for (int i = 0; i < value.Count; i++)
                Putt(i);
        }
        public void Putt(int index)
        {
            if (taint[index] != Contract.TaintState.UnTainted)
                base.ForeColors[index - 1] = Help.TaintOptions.DefaultColors[taint[index]];
        }
        public void SearchTaintValue(FuzzyObject obj)
        {
            if (Value == null) return;
            for (int i = 0; i < value.Count; i++)
                if (value[i] == obj)
                    if (taint[i] == Contract.TaintState.UnTainted)
                    {
                        taint[i] = Contract.TaintState.ChildTainted;
                        Putt(i);
                    }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ListBoxContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "ListBoxContainer";
            this.SelectedIndexChanged += new System.EventHandler(this.ListBoxContainer_SelectedIndexChanged);
            this.ResumeLayout(false);

        }

        private void ListBoxContainer_SelectedIndexChanged(object sender, EventArgs e)
        {
            PushChild();
        }
        private void TextManager_TextChanged(object sender, EventArgs e)
        {
            base.ListBox.Refresh();
        }
    }
    public class ListBoxArgs : ContainerArgs
    {
        public ListBoxArgs()
            : base()
        {
            this.Dock = 5;
        }
        public ListBoxArgs(System.Xml.XmlNode node)
            : base(node)
        {
            this.Dock = 5;
        }
        public int Dock { get; set; }
        public string Show { get; set; }
        protected override void OnScan(string Name, string InnerText)
        {
            base.OnScan(Name, InnerText);
            if (Name == "DOCK")
                this.Dock = GetInt(InnerText);
            else if (Name == "SHOW")
                this.Show = InnerText;

        }
    }
}
