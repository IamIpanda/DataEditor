using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DataEditor.Arce.Interpreter;

namespace DataEditor.Control.Container
{
    public class TabPagesContainer : WrapBaseContainer<TabPagesArgs>
    {
        public override string Flag { get { return "tabs"; } }
        public override void Bind ()
        {
            Binding = new System.Windows.Forms.TabControl();
        }
        public override void Reset ()
        {
            if ( Binding == null || argument == null ) return;
            Binding.Dock = (DockStyle)argument.Dock;
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

    public class TabPageContainer : WrapBaseContainer<TabPageArgs>
    {
        public override string Flag { get { return "tab"; } }
        public override void Bind ()
        {
            Binding = new System.Windows.Forms.TabPage();
        }
    }
    public class TabPageArgs : ContainerArgs 
    {
        public TabPageArgs() : base() { }
        public TabPageArgs(System.Xml.XmlNode node) : base(node) { }
    }
}
