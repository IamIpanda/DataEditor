using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace DataEditor.Control.Window
{
    public partial class _DataEditorDialog_Column : Form
    {
        public _DataEditorDialog_Column ()
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
        public int ExtraWidth { get { return this.ClientSize.Width - panel1.Width; } }
        public event EventHandler OnOKClicked;
        public event EventHandler OnCancelClicked;
    }
}