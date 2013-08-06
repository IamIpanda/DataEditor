using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DataEditor.Arce.Interpreter;

namespace DataEditor.Control.Container
{
    public class TabPagesContainer : TabControl, Control.ObjectContainer
    {
        ContainerHelper Helper = new ContainerHelper();
        TabPagesArgs argument;
        public string Flag { get { return "tabs"; } }
        public Label Label { get; set; }
        public FuzzyData.FuzzyObject Value
        {
            get { return Helper.ChildValue; }
            set { Helper.ChildValue = value; Pull(); }
        }
        public new FuzzyData.FuzzyObject Parent
        {
            get { return Helper.ParentValue; }
            set { Helper.ParentValue = value; Pull(); }
        }
        public ControlArgs Arguments 
        {
            get { return argument; }
            set { argument = value as TabPagesArgs; Reset(); }
        }
        public ControlArgs Load_Information(System.Xml.XmlNode Node)
        {
            TabPagesArgs gba = new TabPagesArgs();
            gba.Label = 0;
            gba.Load(Node);
            // ==============================
            // 处理各个节点，依次加入TabPage
            // ==============================
            foreach (System.Xml.XmlNode ChildNode in Node.ChildNodes)
            {
                TabPageContainer child = new TabPageContainer();
                ControlArgs arg = child.Load_Information(ChildNode);
                child.Arguments = arg;
                this.TabPages.Add(child);
            }
            return gba;
        }
        public void Pull()
        {
            foreach (TabPage page in this.TabPages)
            {
                TabPageContainer child = page as TabPageContainer;
                if (child != null)
                    child.Parent = Helper.ChildValue;
            }
        }
        public void Push() { }
        public void Reset()
        {
            if (argument == null) return;
            ControlHelper.Reset(this, argument);
            if (argument.BackColor != default(System.Drawing.Color))
                this.BackColor = argument.BackColor;
            if (argument.Dock != -1)
                this.Dock = (System.Windows.Forms.DockStyle)argument.Dock;
            Helper.ChildSymbol = argument.Actual;
        }
    }
    public class TabPagesArgs : ContainerArgs
    {
        public TabPagesArgs()
            : base()
        {
            this.Dock = 5;
        }
        public TabPagesArgs(System.Xml.XmlNode node)
            : base(node)
        {
            this.Dock = 5;
        }
        public int Dock { get; set; }
        protected override void OnScan(string Name, string InnerText)
        {
            base.OnScan(Name,InnerText);
            if (Name == "DOCK")
                this.Dock = GetInt(InnerText);
        }
    }

    public class TabPageContainer : TabPage, Control.ObjectContainer 
    {
        ContainerHelper Helper = new ContainerHelper();
        TabPageArgs argument;
        public string Flag { get { return "tab"; } }
        public Label Label { get; set; }
        public FuzzyData.FuzzyObject Value
        {
            get { return Helper.ChildValue; }
            set { Helper.ChildValue = value; Pull(); }
        }
        public new FuzzyData.FuzzyObject Parent
        {
            get { return Helper.ParentValue; }
            set { Helper.ParentValue = value; Pull(); }
        }
        public ControlArgs Arguments 
        {
            get { return argument; }
            set { argument = value as TabPageArgs; Reset(); }
        }
        public ControlArgs Load_Information(System.Xml.XmlNode Node)
        {            
            Builder builder = new Builder();
            System.Drawing.Size size = builder.Build(Node, this.Controls);
            TabPageArgs gba = new TabPageArgs();
            gba.Label = 0;
            gba.Load(Node);
            return gba;
        }
        public void Pull()
        {
            foreach (System.Windows.Forms.Control control in this.Controls)
                if (control is ObjectEditor)
                    (control as ObjectEditor).Parent = Helper.ChildValue;
        }
        public void Push() { }
        public void Reset()
        {
            if (argument == null) return;
            ControlHelper.Reset(this, argument);
            if (argument.BackColor != default(System.Drawing.Color))
                this.BackColor = argument.BackColor;
            Helper.ChildSymbol = argument.Actual;
            this.Text = argument.Text;
        }
    }
    public class TabPageArgs : ContainerArgs { }
}
