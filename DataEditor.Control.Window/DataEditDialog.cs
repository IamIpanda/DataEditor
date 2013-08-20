using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace DataEditor.Control.Window
{
    public partial class DataEditDialog : Form
    {
        public DataEditDialog ()
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
        protected DataEditDialog ded;
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
            ded = new DataEditDialog();
            ded.OnOKClicked += this.OnOKClicked;
            Binding = ded;
        }
        protected override System.Windows.Forms.Control.ControlCollection Controls
        { get { return ded != null ? ded.ChildControls : null; } }
        protected override void SetSize (Size size)
        { if ( ded != null ) ded.ClientSize = new Size(size.Width, size.Height + ded.ExtraHeight); }
        protected void OnOKClicked (object sender, EventArgs e) { origin &= Value; }
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