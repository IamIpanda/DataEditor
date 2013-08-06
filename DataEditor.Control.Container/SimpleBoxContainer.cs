using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DataEditor.Arce.Interpreter;

namespace DataEditor.Control.Container
{
    public class SimpleContainer : System.Windows.Forms.Control, Control.ObjectContainer,Contract.TaintableSingleEditor
    {
        ContainerHelper Helper = new ContainerHelper();
        SimpleBoxArgs argument;
        Contract.TaintState taint;
        public string Flag { get { return "simple"; } }
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
            set { argument = value as SimpleBoxArgs; Reset(); }
        }
        public ControlArgs Load_Information(System.Xml.XmlNode Node)
        {
            Builder builder = new Builder();
            System.Drawing.Size size = builder.Build(Node, this.Controls);
            this.SetClientSizeCore(size.Width, size.Height);
            SimpleBoxArgs gba = new SimpleBoxArgs();
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
        public void Push() { }
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
            Helper.ChildSymbol = argument.Actual;
            this.Text = argument.Text;
        }
    }
    public class SimpleBoxArgs : ContainerArgs 
    {

    }
}
