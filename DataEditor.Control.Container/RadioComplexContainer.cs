using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DataEditor.Arce.Interpreter;

namespace DataEditor.Control.Container
{
    public class RadioComplexContainer : Prototype.ProtoRadioContainer, Control.ComplexContainer
    {
        ComplexContainerHelper Helper = new ComplexContainerHelper();
        RadioComplexContainerArgs argument;
        public Contract.TaintState taint;
        public string Flag { get { return "radio"; } }
        public Label Label { get; set; }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [System.ComponentModel.Browsable(false)]
        public FuzzyData.FuzzyObject Value
        {
            get { return Helper.ChildValue; }
            set { Helper.ChildValue = value; Pull(); }
        }
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [System.ComponentModel.Browsable(false)]
        public new FuzzyData.FuzzyObject Parent
        {
            get { return Helper.ParentValue; }
            set { Helper.ParentValue = value; Pull(); }
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
            Tainted = Help.TaintRecord.Single[Value];
        }
        public void Push()
        {
            
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
            if (argument.Key != "")
                Prototype.RadioGroup.AddRadios(argument.Key, this.Radio);
            Helper.ChildSymbol = argument.Actual;
            this.Text = argument.Text;
        }
    }

    class RadioComplexContainerArgs : ComplexContainerArgs
    {
        public int RadioWidth { get; set; }
        public string Key { get; set; }
        public RadioComplexContainerArgs() : base() { this.Label = 0; RadioWidth = 100; Key = ""; }
        public RadioComplexContainerArgs(System.Xml.XmlNode node) : base(node) { this.Label = 0; RadioWidth = 100; Key = ""; }
        protected override void OnScan(string Name, string InnerText)
        {
            if (Name == "RADIOWIDTH")
                RadioWidth = GetInt(InnerText);
            else if (Name == "KEY")
                Key = InnerText;
            base.OnScan(Name, InnerText);
        }
    }
}
namespace DataEditor.Control.ProtoType
{
    public partial class RadioGroup
    {
        public void Load_Information(System.Xml.XmlNode Node)
        {
 
        }
    }
}
