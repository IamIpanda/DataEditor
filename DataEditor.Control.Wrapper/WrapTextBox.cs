using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Wrapper
{
    public class WrapTextBox : DataEditor.Control.Prototype.ProtoAutoSizeTextBox, ObjectEditor,
        Contract.TaintableSingleEditor,Contract.FocusDependent,Contract.TaintableTrigger
    {
        TextBoxArgs argument;
        FuzzyData.FuzzyString value;
        FuzzyData.FuzzySymbol key;
        Contract.TaintState taint;
        public ControlArgs Load_Information(System.Xml.XmlNode Node) { 
            return new TextBoxArgs(Node); }
        public string Flag { get { return "text"; } }
        public Label Label { get; set; }
        public event EventHandler Taint;
        public ControlArgs Arguments
        {
            get { return argument; }
            set { argument = value as TextBoxArgs; Reset(); }
        }
        public FuzzyData.FuzzyObject Value
        {
            get { return value; }
            set { this.value = value as FuzzyData.FuzzyString; Pull(); }
        }
        public Contract.TaintState Tainted
        {
            get { return taint; }
            set { taint = value; Putt(); }
        }
        public new FuzzyData.FuzzyObject Parent
        {
            set
            {
                FuzzyData.FuzzyString num = ControlHelper.TypeCheck<FuzzyData.FuzzyString>.Get(value, key);
                if (num != null) Value = num;
                Pull();
            }
        }
        public void Push()
        {
            if (value != null)
                value.Text = this.Text;
        }
        public void Reset()
        {
            if (argument == null) return;
            DataEditor.Control.ControlHelper.Reset(this, argument);
            this.key = argument.Actual;
        }
        public void Pull()
        {
            if (value != null)
            {
                this.Text = value.Text;
                Tainted = Help.TaintRecord.Single[value];
            }
        }
        public void Putt()
        {
            Help.TaintHelper.OnPutt(Label, taint);
        }

        public void OnEnter(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.Color.FromArgb(239, 228, 176);
            this.ForeColor = System.Drawing.Color.FromArgb(119, 100, 9);
        }

        public void OnLeave(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.Color.White;
            this.ForeColor = System.Drawing.Color.Black;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            if (this.Focused && Taint != null) Taint(this, e);
        }
    }
    public class TextBoxArgs : ControlArgs
    {
        public TextBoxArgs() : base() { }
        public TextBoxArgs(System.Xml.XmlNode node) : base(node) { }
    }
}