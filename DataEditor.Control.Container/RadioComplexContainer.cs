using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DataEditor.Arce.Interpreter;

namespace DataEditor.Control.Container
{
    /*
    public class RadioComplexContainer : Prototype.ProtoRadioContainer, Control.ComplexContainer
    {
        ComplexContainerHelper Helper = new ComplexContainerHelper();
        RadioComplexContainerArgs argument;
        FuzzyData.FuzzySymbol key;
        FuzzyData.FuzzyFixnum value;
        int SelfValue;

        public Contract.TaintState taint;
        public string Flag { get { return "radio"; } }
        public Label Label { get; set; }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [System.ComponentModel.Browsable(false)]
        public FuzzyData.FuzzyObject ContainerValue
        {
            get { return Helper.ChildValue; }
            set { Helper.ChildValue = value; Pull(); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [System.ComponentModel.Browsable(false)]
        public FuzzyData.FuzzyObject Value
        {
            get { return value; }
            set { this.value = value as FuzzyData.FuzzyFixnum; Pull(); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [System.ComponentModel.Browsable(false)]
        public new FuzzyData.FuzzyObject Parent
        {
            get { return Helper.ParentValue; }
            set {
                Helper.ParentValue = value;
                FuzzyData.FuzzyFixnum num = ControlHelper.TypeCheck<FuzzyData.FuzzyFixnum>.Get(value, key);
                if (num != null) Value = num;
                Pull();
            }
        }
        public Contract.TaintState Tainted
        {
            get { return taint; }
            set { taint = value; Putt(); }
        }
        public ControlArgs Arguments
        {
            get { return argument; }
            set { argument = value as RadioComplexContainerArgs; Reset(); }
        }
        public ControlArgs Load_Information(System.Xml.XmlNode Node)
        {
            Builder builder = new Builder();
            System.Drawing.Size size = builder.Build(Node, this.Controls);
            this.SetClientSizeCore(size.Width + this.RadioWidth, size.Height + 6);
            RadioComplexContainerArgs gba = new RadioComplexContainerArgs();
            gba.Load(Node);
            return gba;
        }
        public void Pull()
        {
            foreach (System.Windows.Forms.Control control in this.Controls)
                if (control is ObjectEditor)
                    (control as ObjectEditor).Parent = Helper.ChildValue;
            if (value.Value == SelfValue)
                this.Radio.Checked = true;
            Tainted = Help.TaintRecord.Single[Value];
        }
        public void Push()
        {
            if (base.Radio.Checked)
                if (value != null)
                    value.Value = SelfValue;
        }
        public void Putt()
        {
            Help.TaintHelper.OnPutt(Label, taint);
        }
        public void Reset()
        {
            if (argument == null) return;
            ControlHelper.Reset(this, argument);
            if (argument.BackColor != default(System.Drawing.Color))
                this.BackColor = argument.BackColor;
            if (argument.RadioWidth > 0)
                this.RadioWidth = argument.RadioWidth;
            RadioGroup.AddRadio(argument.Group, this.Radio);    
            this.SelfValue = argument.Value;
            this.key = argument.Real;
            this.Text = argument.Text;
            Helper.ChildSymbol = argument.Actual;
        }
    }

    public class RadioComplexContainerArgs : ComplexContainerArgs
    {
        public int RadioWidth { get; set; }
        public string Group { get; set; }
        public int Value { get; set; }
        public RadioComplexContainerArgs() : base() 
        {
            this.Label = 0;
            this.RadioWidth = 100;
            this.Group = "";
            this.Value = -10;
        }
        public RadioComplexContainerArgs(System.Xml.XmlNode node) : base(node) 
        {
            this.Label = 0;
            this.RadioWidth = 100;
            this.Group = "";
            this.Value = -10;
        }
        protected override void OnScan(string Name, string InnerText)
        {
            if (Name == "RADIOWIDTH")
                RadioWidth = GetInt(InnerText);
            else if (Name == "GROUP")
                Group = InnerText;
            else if (Name == "VALUE")
                Value = GetInt(InnerText);
            base.OnScan(Name, InnerText);
        }
    }
    public partial class RadioGroup
    {
        string Key;
        List<RadioButton> radios = new List<RadioButton>();
        void CheckedChanged(object sender, EventArgs e)
        {
            RadioButton r = sender as RadioButton;
            if (r == null || r.Checked == false) return;
            foreach (RadioButton rb in radios)
                if (rb != null && rb != r) rb.Checked = false;
        }
        public static void AddRadio(string key, RadioButton radio)
        {
            if (!(groups.ContainsKey(key)))
            {
                groups.Add(key, new RadioGroup(key));
                Help.Log.log("添加了下述的 RadioGroup：" + key);
            }
            groups[key].AddRadio(radio);
        }
        public void AddRadio(RadioButton radio)
        {
            if (!(radios.Contains(radio)))
            {
                radios.Add(radio);
                radio.CheckedChanged += this.CheckedChanged;
            }
        }
        protected RadioGroup(string Key) { this.Key = Key; }
        protected static Dictionary<string, RadioGroup> groups = new Dictionary<string, RadioGroup>();
    }
     */
}