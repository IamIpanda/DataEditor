using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Window
{
    public partial class SimpleRadioChoose : Form
    {
        public SimpleRadioChoose()
        {
            InitializeComponent();
        }

        public SimpleRadioChoose(string Title, string Description, List<String> Options, int Index)
        {
            InitializeComponent();
            ShowDialog(Title, Description, Options, Index);
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        public int SelectedIndex
        {
            get { return protoListBox1.SelectedIndex; }
        }
        public DialogResult ShowDialog(string Title, string Description, List<String> Options,int Index)
        {
            this.Text = Title;
            lbDescription.Text = Description;
            protoListBox1.Items.Clear();
            foreach (string str in Options)
                protoListBox1.Items.Add(str);
            protoListBox1.SelectedIndex = Index;
            return base.ShowDialog();
        }
    }
}
