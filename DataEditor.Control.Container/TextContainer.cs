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
        public new ControlCollection SControls
        {
            get { return panel1.Controls; }
        }
        public override string Text
        {
            get { return lbText.Text; }
            set {
                lbText.Text = value; }
        }
        public int DeltaHeight { get { return Height - panel1.Height; } }
    }
    public class TextContainer : WrapBaseContainer<TextContainerArgs>
    {
        public override string Flag { get { return "box"; } }
        public override void Bind ()
        {
            Binding = new _TextContainer();    
        }
        protected override System.Windows.Forms.Control.ControlCollection Controls
        { get { return (Binding as _TextContainer).SControls; } }
        protected override void SetSize (Size size)
        {
            int height = size.Height + (Binding as _TextContainer).DeltaHeight;
            base.SetSize(new Size(size.Width, height));
        }
    }
    public class TextContainerArgs : ContainerArgs
    {
        public TextContainerArgs () { }
        public TextContainerArgs (System.Xml.XmlNode node) : this() { Load(node); }
    }
}
