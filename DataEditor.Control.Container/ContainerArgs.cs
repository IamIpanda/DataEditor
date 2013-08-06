using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Container
{
    public class ContainerArgs : ControlArgs
    {
        public ContainerArgs()
            : base()
        {
            this.BackColor = default(System.Drawing.Color);
        }
        public ContainerArgs(System.Xml.XmlNode node)
            : base(node)
        {
            this.BackColor = default(System.Drawing.Color);
        }
        public System.Drawing.Color BackColor { get; set; }
        protected override void OnScan(string Name, string InnerText)
        {
            base.OnScan(Name,InnerText);
            if (Name == "BACK")
                BackColor = GetColor(InnerText);
        }
    }
}
