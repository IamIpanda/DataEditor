using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DataEditor.Arce.Interpreter;

namespace DataEditor.Control.Container
{
    public class GroupBoxContainer : GroupBox, Control.ObjectContainer
    {
        ContainerHelper Helper = new ContainerHelper();
        GroupBoxArgs argument;
        public string Flag { get { return "group"; } }
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
            set { argument = value as GroupBoxArgs; Reset(); }
        }
        public ControlArgs Load_Information(System.Xml.XmlNode Node)
        {
            Builder builder = new Builder();
            System.Drawing.Size size = builder.Build(Node, this.Controls, 2, 13);
            this.SetClientSizeCore(size.Width, size.Height);
            GroupBoxArgs gba = new GroupBoxArgs();
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
            System.Windows.Forms.MessageBox.Show(this.ClientRectangle.ToString());
            if (argument == null) return;
            ControlHelper.Reset(this, argument);
            if (argument.BackColor != default(System.Drawing.Color))
                this.BackColor = argument.BackColor;
            if (argument.Dock != -1)
                this.Dock = (DockStyle)argument.Dock;
            Helper.ChildSymbol = argument.Actual;
            this.Text = argument.Text;
        }
    }
    public class GroupBoxArgs : ContainerArgs
    {
        public GroupBoxArgs()
            : base()
        {
            this.Dock = 5;
        }
        public GroupBoxArgs(System.Xml.XmlNode node)
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
}
