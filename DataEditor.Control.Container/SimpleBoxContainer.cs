using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DataEditor.Arce.Interpreter;

namespace DataEditor.Control.Container
{
    public class SimpleContainer : WrapBaseContainer<SimpleBoxArgs>
    {
        public override void Bind ()
        {
            Binding = new System.Windows.Forms.Control();
        }
    }
    public class SimpleBoxArgs : ContainerArgs 
    {
        public SimpleBoxArgs() : base() { }
        public SimpleBoxArgs(System.Xml.XmlNode node) : base(node) { }
    }
}
