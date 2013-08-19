using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DataEditor.Control;


namespace DataEditor.Control.Wrapper
{
    public class WrapNumInput : NumericUpDown, ObjectEditor,Contract.TaintableSingleEditor 
    {
        NumInputArgs argument;
        FuzzyData.FuzzyFixnum value;
        FuzzyData.FuzzySymbol key;
        public ControlArgs Load_Information(System.Xml.XmlNode Node) { return new NumInputArgs(Node); }
        public ControlArgs Arguments
        {
            get { return argument; }
            set { argument = value as NumInputArgs; Reset(); }
        }
        public new FuzzyData.FuzzyObject Value
        {
            get { return value; }
            set { this.value = value as FuzzyData.FuzzyFixnum; Pull(); }
        }
        protected Contract.TaintState tainted;
        public Contract.TaintState Tainted
        {
            get { return tainted; }
            set { tainted = value; Putt(); }
        }
        public new FuzzyData.FuzzyObject Parent
        {
            set
            {
                FuzzyData.FuzzyFixnum num = ControlHelper.TypeCheck<FuzzyData.FuzzyFixnum>.Get(value, key);
                if (num != null) Value = num;
                Pull();
            }
        }
        public void Push()
        {
            if (value != null)
                value.Value = Convert.ToInt64(base.Value);
        }
        public void Pull()
        {
            if (value == null) return;
            long num = value.Value;
            if (num > Maximum)
                base.Value = Maximum;
            else if (num < Minimum)
                base.Value = Minimum;
            else
                base.Value = num;
            this.Tainted = Help.TaintRecord.Single[value];
        }
        public void Putt()
        {
            Help.TaintHelper.OnPutt(Label, tainted);
        }
        public void Reset()
        {
            ControlHelper.Reset(this, argument);
            if (argument == null) return;
            this.Minimum = argument.MinValue;
            this.Maximum = argument.MaxValue;
            this.key = argument.Actual;
        }
        public string Flag { get { return "int"; } }
        public Label Label { get; set; }
        
    }
    public class NumInputArgs : ControlArgs
    {
        public NumInputArgs()
            : base()
        {
            this.MaxValue = int.MaxValue;
            this.MinValue = 0;
        }
        public NumInputArgs(System.Xml.XmlNode node)
            : base(node)
        {
            this.MaxValue = int.MaxValue;
            this.MinValue = 0;
        }
        public int MaxValue { get; set; }
        public int MinValue { get; set; }
        protected override void OnScan(string Name, string InnerText)
        {
            base.OnScan(Name, InnerText);
            if (Name == "MAXVALUE")
                MaxValue = GetInt(InnerText);
            else if (Name == "MINVALUE")
                MinValue = GetInt(InnerText);
        }
    }
    /*
    public class ObjectEditorHelper<ArgumentClass, DataClass>
        where ArgumentClass : ControlArgs, new()
        where DataClass : FuzzyData.FuzzyObject
    {
        ArgumentClass argument;
        DataClass value;
        FuzzyData.FuzzySymbol key;
        public ControlArgs Load_Information(System.Xml.XmlNode Node)
        {
            ArgumentClass argument = new ArgumentClass();
            argument.Load(Node);
            return argument;
        }
        public ControlArgs Arguments
        {
            get { return argument; }
            set { argument = value as ArgumentClass; }
        }
        public new FuzzyData.FuzzyObject Value
        {
            get { return value; }
            set { this.value = value as DataClass; }
        }
        public new FuzzyData.FuzzyObject Parent
        {
            set
            {
                FuzzyData.FuzzyFixnum num = ControlResetHelper.TypeCheck<FuzzyData.FuzzyFixnum>.Get(value, key);
                if (num != null) Value = num;
            }
        }
    }
     * */
}