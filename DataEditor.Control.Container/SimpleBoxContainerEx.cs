using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Container
{
    public partial class SimpleBoxContainerEx : UserControl, Control.ObjectContainer,Contract.TaintableSingleEditor
    {
        public SimpleBoxContainerEx()
        {
            InitializeComponent();
        }
        public string Flag { get { return "box"; } }
        public virtual Label Label { get; set; }
        public FuzzyData.FuzzyObject Value
        {
            get { return simpleContainer1.Value; }
            set { simpleContainer1.Value = value; Pull(); }
        }
        public new FuzzyData.FuzzyObject Parent
        {
            get { return simpleContainer1.Parent; }
            set { simpleContainer1.Parent = value; Pull(); }
        }
        public Contract.TaintState Tainted
        {
            get { return simpleContainer1.Tainted; }
            set { simpleContainer1.Tainted = value; Putt(); }
        }
        public ControlArgs Arguments
        {
            get { return simpleContainer1.Arguments; }
            set { simpleContainer1.Arguments = value; Reset(); }
        }
        public ControlArgs Load_Information(System.Xml.XmlNode Node)
        {
            ControlArgs arg = simpleContainer1.Load_Information(Node);
            this.SetClientSizeCore(simpleContainer1.Width, simpleContainer1.Height + 16);
            return arg;
        }
        public void Pull()
        {
            Tainted = Help.TaintRecord.Single[this.Value];
        }
        public void Push() 
        {
            simpleContainer1.Push();
        }
        public void Putt()
        {
            Contract.TaintState Tainted = Help.TaintRecord.Single[Value];
            lbName.ForeColor = Help.TaintOptions.DefaultColors[Tainted];
        }
        public void Reset()
        {
            if (simpleContainer1.Arguments != null)
            {
                SimpleBoxArgs sba = simpleContainer1.Arguments as SimpleBoxArgs;
                lbName.Text = sba.Text;
            }
        }

        private void simpleContainer1_BackColorChanged(object sender, EventArgs e)
        {
            this.BackColor = simpleContainer1.BackColor;
            lbName.BackColor = simpleContainer1.BackColor;
        }
    }
}
