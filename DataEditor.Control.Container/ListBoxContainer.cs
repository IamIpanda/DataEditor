using System;
using System.Collections.Generic;
using System.Text;
using DataEditor.FuzzyData;
using System.Windows.Forms;

namespace DataEditor.Control.Container
{
    public class ListBoxContainer : WrapBaseContainer<ListBoxArgs>
    {
        string model;
        Help.LinkTable<int, int> Link = new Help.LinkTable<int, int>();
        List<Help.TextManager> TextManager = new List<Help.TextManager>();

        public override string Flag { get { return "list"; } }
        public override void Bind ()
        {
            var lb = new Prototype.ProtoFullListBox();
            lb.SelectedIndexChanged += OnSelectedIndexChanged;
            Binding = lb;
        }
        public override void Reset ()
        {
            base.Reset();
            if ( argument == null || Binding == null ) return;
            this.model = argument.Show;

            Binding.Dock = (DockStyle)argument.Dock;
            (Binding as Prototype.ProtoFullListBox).Text = argument.Text;
        }
        public override FuzzyObject Value
        {
            get
            {
                Prototype.ProtoFullListBox origin = Binding as Prototype.ProtoFullListBox;
                if (origin == null) return null;
                Prototype.ProtoLeftListBox lb = origin.ListBox;
                FuzzyArray arr = base.Value as FuzzyArray;
                if ( lb == null || arr == null ) return null;
                if ( lb.SelectedIndex == -1 ) return null;
                else return arr[Link.Reverse[lb.SelectedIndex]] as FuzzyObject;
            }
            set { throw new NotImplementedException(); }
        }
        public override void Pull ()
        {
            if ( base.Value == null || model == null || Binding == null ) return;
            FuzzyArray arr = base.Value as FuzzyArray;
            Prototype.ProtoFullListBox origin = Binding as Prototype.ProtoFullListBox;
            if ( arr == null || arr == null ) return;
            Prototype.ProtoLeftListBox lb = origin.ListBox;
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
            base.Pull();
        }

        private void OnSelectedIndexChanged (object sender, EventArgs e)
        {
            base.Pull();
        }
        protected override System.Windows.Forms.Control.ControlCollection Controls
        { get { return (Binding as Prototype.ProtoFullListBox).Controls; } }
        protected override int ExtraHeight { get { return 6; } }
        protected override int ExtraWidth { get { return 6; } }
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
