using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Wrapper
{
    public class WrapTextBox : DataEditor.Control.WrapBaseEditor<FuzzyData.FuzzyString, TextBoxArgs>
    {
        public override string Flag
        {
            get { return "text"; }
        }
        public override void Pull ()
        {
            TextBox tb = Binding as TextBox;
            if ( tb != null ) tb.Text = value.Text;
        }
        public override void Push ()
        {
            TextBox tb = Binding as TextBox;
            if ( tb != null ) value.Text = tb.Text;
        }
        public override void Bind ()
        {
            Binding = new Prototype.ProtoAutoSizeTextBox();
        }
    }
    
    public class TextBoxArgs : ControlArgs
    {
        public TextBoxArgs() : base() { }
        public TextBoxArgs(System.Xml.XmlNode node) : base(node) { }
    }
}