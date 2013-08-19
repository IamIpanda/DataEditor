using System;
using System.Collections.Generic;
using System.Text;
using DataEditor.FuzzyData;
using System.Windows.Forms;

namespace DataEditor.Control.Container
{
    public class _ListBoxContainer : Prototype.ProtoFullListBox
    {   
        public _ListBoxContainer()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ListBoxContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "ListBoxContainer";
            this.ResumeLayout(false);
        }
    }
    public class ListBoxContainer : WrapBaseContainer<ListBoxArgs>
    {
        string model;
        Help.LinkTable<int, int> Link = new Help.LinkTable<int, int>();
        List<Help.TextManager> TextManager = new List<Help.TextManager>();
        public override string Flag { get { return "list"; } }
        public override void Bind ()
        {
            Binding = new _ListBoxContainer();
        }
        public override void Reset ()
        {
            base.Reset();
            if ( argument == null || Binding == null ) return;
            this.model = argument.Show;

            Binding.Dock = (DockStyle)argument.Dock;
            Binding.Text = argument.Text;
        }
        public override FuzzyObject Value
        {
            get
            {
                ListBox lb = Binding as ListBox;
                FuzzyArray arr = value as FuzzyArray;
                if ( lb == null || arr == null ) return null;
                if ( lb.SelectedIndex == -1 ) return null;
                if ( value == null ) return null;
                else return arr[Link.Reverse[lb.SelectedIndex]] as FuzzyObject;
            }
            set { throw new NotImplementedException(); }
        }
        public override void Pull ()
        {
            if ( value == null || model == null || Binding == null ) return;
            FuzzyArray arr = value as FuzzyArray;
            ListBox lb = Binding as ListBox;
            if ( lb == null || arr == null ) return;
            Link.Clear();
            TextManager.Clear();
            lb.Items.Clear();
            int i = -1, j = 0;
            foreach ( object Child in arr )
            {
                i++;
                if ( Child == null || Child is FuzzyNil ) continue;
                FuzzyObject child = Child as FuzzyObject;
                if ( child == null ) continue;
                Help.TextManager text = new Help.TextManager(model, child);
                lb.Items.Add(text);
                Link.Add(i, j++);
            }
        }
    }
    public class ListBoxArgs : ContainerArgs
    {
        public ListBoxArgs()
            : base()
        {
            this.Dock = 5;
            this.Label = 0;
        }
        public ListBoxArgs (System.Xml.XmlNode node) : this() { Load(node); }
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
