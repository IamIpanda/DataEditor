using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Container
{
    public partial class _TextContainer : UserControl
    {
        public _TextContainer ()
        {
            InitializeComponent();
        }
        public new ControlCollection Controls
        {
            get { return panel1.Controls; }
        }
        public override string Text
        {
            get { return lbText.Text; }
            set { lbText.Text = value; }
        }
    }
    public class TextContainer : WrapBaseContainer<TextContainerArgs>
    {
        public override string Flag { get { return "box"; } }
        public override void Bind ()
        {
            Binding = new _TextContainer();    
        }
    }
    public class TextContainerArgs : ContainerArgs
    {
        public TextContainerArgs () { }
        public TextContainerArgs (System.Xml.XmlNode node) : this() { Load(node); }
    }
}
