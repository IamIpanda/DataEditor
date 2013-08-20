using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace DataEditor.Control.Window
{
    public partial class _DataEditorDialog_Row : Form
    {
        public _DataEditorDialog_Row ()
        {
            InitializeComponent();
        }

        protected virtual void button1_Click (object sender, EventArgs e)
        {
            if ( OnOKClicked != null ) OnOKClicked(this, e);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        protected virtual void button2_Click (object sender, EventArgs e)
        {
            if ( OnCancelClicked != null ) OnCancelClicked(this, e);
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        public System.Windows.Forms.Control.ControlCollection ChildControls { get { return panel1.Controls; } }
        public int ExtraHeight { get { return this.ClientSize.Height - panel1.Height; } }
        public event EventHandler OnOKClicked;
        public event EventHandler OnCancelClicked;
    }

    public class DataEditorDialog : Container.WrapBaseContainer<EditorWindowArgs>
    {
        protected _DataEditorDialog_Row dedr;
        protected _DataEditorDialog_Column dedc;
        public override string Flag { get { return "e-window"; } }
        protected FuzzyData.FuzzyObject origin;

        public override FuzzyData.FuzzyObject Value
        {
            get { return base.Value; }
            set { origin = value; base.Value = value.Clone() as FuzzyData.FuzzyObject; }
        }
        public override FuzzyData.FuzzyObject Parent
        {
            set { origin = value; base.Parent = value.Clone() as FuzzyData.FuzzyObject; }
        }
        public override void Bind ()
        {
            dedr = new _DataEditorDialog_Row();
            dedc = new _DataEditorDialog_Column();
            dedr.OnOKClicked += this.OnOKClicked;
            dedc.OnOKClicked += this.OnOKClicked;
            Binding = dedc;
        }
        protected override System.Windows.Forms.Control.ControlCollection Controls
        {
            get 
            {
                if ( Binding == dedr && dedr != null ) return dedr.ChildControls;
                else if ( Binding == dedc && dedc != null ) return dedc.ChildControls;
                return null;
            }
        }
        protected override void SetSize (Size size)
        {
            if ( Binding == dedr && dedr != null )
                dedr.ClientSize = new Size(size.Width, size.Height + dedr.ExtraHeight);
            else if ( Binding == dedc && dedc != null )
                dedc.ClientSize = new Size(size.Width + dedc.ExtraWidth, size.Height + 8);
        }
        protected void OnOKClicked (object sender, EventArgs e) { origin &= Value; }
        public void Switch () { if ( Binding == dedc ) Binding = dedr; else Binding = dedc; }
    }
    public class EditorWindowArgs : DataEditor.Control.Container.SimpleBoxArgs
    {
        public EditorWindowArgs () : base() { }
        public EditorWindowArgs (System.Xml.XmlNode node) : base(node) { }
        public EditorWindowArgs (Container.SimpleBoxArgs arg)
        {
            this.Actual = arg.Actual;
            this.BackColor = arg.BackColor;
            this.Height = arg.Height;
            this.Label = 0;
            this.Text = arg.Text;
            this.Width = arg.Width;
        }
    }
}