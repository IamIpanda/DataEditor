using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Wrapper
{
    public class WrapCheckBox : WrapBaseEditor<FuzzyData.FuzzyBool, CheckBoxArgs>
    {
        CheckBox actual;
        public override string Flag { get { return "check"; } }
        public override void Bind () { Binding = actual = new CheckBox(); }
        public override void Pull () { if ( actual != null && value != null ) actual.Checked = value.Value; }
        public override void Push () { if ( actual != null && value != null ) value.Value = actual.Checked; }
        protected override bool CheckValue () { if ( actual != null && value != null ) return (!actual.Checked == value.Value); return false; }
        protected override void ShowTaint (Contract.TaintState State) { if ( actual != null && value != null ) actual.ForeColor = Help.TaintOptions.DefaultColors[State]; }
    }
    public class CheckBoxArgs : ControlArgs
    {
        public CheckBoxArgs () : base() { Label = 0; }
        public CheckBoxArgs (System.Xml.XmlNode node) : this() { Load(node); }
    }
}
