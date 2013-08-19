using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DataEditor.Arce.Interpreter;

namespace DataEditor.Control.Container
{
    public class GroupBoxContainer : WrapBaseContainer<GroupBoxArgs>
    {
        public override string Flag { get { return "group"; } }
        public override void Bind ()
        {
            Binding = new GroupBox();
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
