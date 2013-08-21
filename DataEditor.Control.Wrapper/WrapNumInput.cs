using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DataEditor.Control;


namespace DataEditor.Control.Wrapper
{
    public class WrapNumInput : Control.WrapBaseEditor<FuzzyData.FuzzyFixnum, NumInputArgs>
    {
        NumericUpDown nud;
        public override void Bind ()
        {
            Binding = nud = new NumericUpDown();
        }
        public override string Flag { get { return "int"; } }
        public override void Push ()
        {
            if ( value != null && nud != null )
                value.Value = Convert.ToInt64(nud.Value);
        }
        public override void Pull ()
        {
            if ( value == null || nud == null ) return;
            long num = value.Value;
            if ( num > nud.Maximum )
                nud.Value = nud.Maximum;
            else if ( num < nud.Minimum )
                nud.Value = nud.Minimum;
            else
                nud.Value = num;
        }
        public override void Reset ()
        {
            base.Reset();
            if ( argument == null ) return;
            nud.Minimum = argument.MinValue;
            nud.Maximum = argument.MaxValue;
        }
        protected override bool CheckValue ()
        {
            if ( nud == null || value == null ) return base.CheckValue();
            return !(nud.Value == value.Value);
        }
    }
    public class NumInputArgs : ControlArgs
    {
        public NumInputArgs ()
            : base()
        {
            this.MaxValue = int.MaxValue;
            this.MinValue = 0;
        }
        public NumInputArgs (System.Xml.XmlNode node)
            : base(node)
        {
            this.MaxValue = int.MaxValue;
            this.MinValue = 0;
        }
        public int MaxValue { get; set; }
        public int MinValue { get; set; }
        protected override void OnScan (string Name, string InnerText)
        {
            base.OnScan(Name, InnerText);
            if ( Name == "MAXVALUE" )
                MaxValue = GetInt(InnerText);
            else if ( Name == "MINVALUE" )
                MinValue = GetInt(InnerText);
        }
    }
}