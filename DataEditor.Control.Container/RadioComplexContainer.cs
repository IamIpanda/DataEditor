using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DataEditor.Interpreter;

namespace DataEditor.Control.Container
{
    
    public class RadioComplexContainer :WrapBaseComplexContainer<FuzzyData.FuzzyFixnum,RadioComplexContainerArgs>
    {
        int SelfValue = -10, RadioWidth = 0;
        Prototype.ProtoRadioContainer prc;
        public override string Flag { get { return "radio"; } }
        public override void Bind () { prc = new Prototype.ProtoRadioContainer(); Binding = prc; }
        protected override void SetSize (System.Drawing.Size size)
        {
            int width = size.Width + RadioWidth;
            base.SetSize(new System.Drawing.Size(width,size.Height));
        }
        public override void Pull()
        {
            base.Pull();
            if (value.Value == SelfValue)
                prc.Radio.Checked = true;
        }
        public override void Push()
        {
            base.Push();
            if (prc.Radio.Checked)
                if (value != null)
                    value.Value = SelfValue;
        }
        public override void Reset()
        {
            base.Reset();
            if ( argument == null || prc == null ) return;
            this.SelfValue = argument.Value;
            this.RadioWidth = argument.RadioWidth;
            RadioGroup.AddRadio(argument.Group, prc.Radio);
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
     
}