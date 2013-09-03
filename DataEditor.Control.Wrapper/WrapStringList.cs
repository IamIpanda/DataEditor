using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper
{
    public class WrapStringList : WrapBaseEditor<Adapter.AdvanceStringList, StringListArgs>
    {
        FastColoredTextBoxNS.FastColoredTextBox actual;
        public override void Push ()
        {
            // TODO : Finish it.
        }

        public override void Pull ()
        {
            if ( actual == null || value == null ) return;
            actual.Text = "";
            foreach ( FuzzyData.FuzzyString str in value.Data )
                actual.Text += str.Text + "\r\n";
            actual.LineNumberStartValue = (uint)Math.Abs(value.StartIndex);
        }

        public override void Bind ()
        {
            Binding = actual = new FastColoredTextBoxNS.FastColoredTextBox();
           
        }
        protected override Adapter.AdvanceStringList ConvertToValue (FuzzyData.FuzzyObject origin)
        {
            FuzzyData.FuzzyArray fa = origin as FuzzyData.FuzzyArray;
            if ( fa == null ) return null;
            Adapter.AdvanceStringList asl = new Adapter.AdvanceStringList(fa);
            if ( asl.Exists ) return asl;
            return null;
        }
        public override string Flag { get { return "stringlist"; } }
    }
    public class StringListArgs : ControlArgs
    {
        public int StartIndex { get; set; }
        public StringListArgs () { StartIndex = 1; }
        public StringListArgs (System.Xml.XmlNode node) : this() { Load(node); }
        protected override void OnScan (string Name, string InnerText)
        {
            if ( Name == "START" ) StartIndex = GetInt(InnerText);
            base.OnScan(Name, InnerText);
        }
    }
}
