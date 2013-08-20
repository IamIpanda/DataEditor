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
        Help.TaintIndexCollection Taint = new Help.TaintIndexCollection(1);

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
                if ( origin == null ) return null;
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
            Help.TaintRecord.Multi[arr] = Taint;
        }

        private void OnSelectedIndexChanged (object sender, EventArgs e)
        {
            base.Pull();
        }
        protected override System.Windows.Forms.Control.ControlCollection Controls
        { get { return (Binding as Prototype.ProtoFullListBox).Controls; } }
        protected override int ExtraHeight { get { return 6; } }
        protected override int ExtraWidth { get { return 6; } }
        protected override void PushTaint () { /* 已弃用 */ }
        protected override void PullTaint () { ShowTaint(); }
        protected override void ShowTaint (Contract.TaintState State) { /* 已弃用 */ }
        protected void PushTaint (int index) { Taint[index] = Contract.TaintState.ChildTainted; }
        protected void ShowTaint () 
        {      
            Prototype.ProtoFullListBox origin = Binding as Prototype.ProtoFullListBox;
            if (origin == null ) return;
            Prototype.ProtoLeftListBox lb = origin.ListBox;
            for ( int i = 0; i < Taint.Count; i++ )
                lb.ForeColors[i] = Help.TaintOptions.DefaultColors[Taint[i]];
        }
        protected void ShowTaint (int i)
        {
            Prototype.ProtoFullListBox origin = Binding as Prototype.ProtoFullListBox;
            if ( origin == null ) return;
            Prototype.ProtoLeftListBox lb = origin.ListBox;
            lb.ForeColors[i] = Help.TaintOptions.DefaultColors[Taint[i]];
        }
        public override void OnChildTainted (ObjectEditor child)
        {
            Prototype.ProtoFullListBox box = Binding as Prototype.ProtoFullListBox;
            if ( box == null ) return;
            int index = box.ListBox.SelectedIndex;

            PushTaint(index);
            ShowTaint(index);
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
