using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DataEditor.Control;


namespace DataEditor.Control.Wrapper
{
    public class WrapNumInput : Control.WrapBaseEditor<FuzzyData.FuzzyFixnum, NumInputArgs>
    {
        NumericUpDown actual;
        public override void Bind ()
        {
            Binding = actual = new NumericUpDown();
        }
        public override string Flag { get { return "int"; } }
        public override void Push ()
        {
            if ( value != null && actual != null )
                value.Value = Convert.ToInt64(actual.Value);
        }
        public override void Pull ()
        {
            if ( value == null || actual == null ) return;
            long num = value.Value;
            if ( num > actual.Maximum )
                actual.Value = actual.Maximum;
            else if ( num < actual.Minimum )
                actual.Value = actual.Minimum;
            else
                actual.Value = num;
        }
        public override void Reset ()
        {
            base.Reset();
            if ( argument == null ) return;
            actual.Minimum = argument.MinValue;
            actual.Maximum = argument.MaxValue;
        }
        protected override bool CheckValue ()
        {
            if ( actual == null || value == null ) return base.CheckValue();
            return !(actual.Value == value.Value);
        }
    }
    public class NumInputArgs : ControlArgs
    {
        public int MaxValue { get; set; }
        public int MinValue { get; set; }
        public NumInputArgs ()
            : base()
        {
            this.MaxValue = int.MaxValue;
            this.MinValue = 0;
        }
        public NumInputArgs (System.Xml.XmlNode node) : this() { Load(node); }
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




namespace DataEditor.Control.Wrapper
{
    public class WrapFloatInput : Control.WrapBaseEditor<FuzzyData.FuzzyFloat, FloatInputArgs>
    {
        NumericUpDown actual;
        double times = 1d;
        public override void Bind ()
        {
            Binding = actual = new NumericUpDown();
        }
        public override string Flag { get { return "float"; } }
        public override void Push ()
        {
            if ( value != null && actual != null )
                value.Value = Convert.ToDouble(actual.Value) / times;
        }
        public override void Pull ()
        {
            if ( value == null || actual == null ) return;
            double num = value.Value * times;
            if ( num > (double)actual.Maximum )
                actual.Value = actual.Maximum;
            else if ( num < (double)actual.Minimum )
                actual.Value = actual.Minimum;
            else
                actual.Value = (decimal)num;
        }
        public override void Reset ()
        {
            base.Reset();
            if ( argument == null ) return;
            actual.Minimum = argument.MinValue;
            actual.Maximum = argument.MaxValue;
            if ( actual.Increment > 0 ) actual.Increment = argument.Increment;
        }
        protected override bool CheckValue ()
        {
            if ( actual == null || value == null ) return base.CheckValue();
            return !((double)actual.Value == value.Value * times);
        }
    }
    public class FloatInputArgs : NumInputArgs
    {
        public int Increment { get; set; }
        public double Times { get; set; }
        protected override void OnScan (string Name, string InnerText)
        {
            base.OnScan(Name, InnerText);
            if ( Name == "INCREMENT" ) Increment = GetInt(InnerText);
            else if ( Name == "TIMES" ) Times = GetDouble(InnerText);
        }
    }
}