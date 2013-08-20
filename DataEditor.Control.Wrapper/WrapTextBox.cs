using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Wrapper
{
    public class WrapTextBox : DataEditor.Control.WrapBaseEditor<FuzzyData.FuzzyString, TextBoxArgs>
    {
        TextBox tb;
        public override string Flag
        {
            get { return "text"; }
        }
        public override void Pull ()
        {
                if ( tb != null && value != null ) tb.Text = value.Text;   
        }
        public override void Push ()
        {
            if ( tb != null ) value.Text = tb.Text;
        }
        public override void Bind ()
        {
            tb = new Prototype.ProtoAutoSizeTextBox();
            Binding = tb;
        }
        protected override bool CheckValue ()
        {
            if ( tb == null || value == null ) return false;
            return !(value.Text == tb.Text);
        }
    }
    
    public class TextBoxArgs : ControlArgs
    {
        public TextBoxArgs() : base() { }
        public TextBoxArgs(System.Xml.XmlNode node) : base(node) { }
    }
}