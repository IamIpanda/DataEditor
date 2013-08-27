using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Window
{
    public partial class SingleImageChoser : Form
    {
        public Adapter.AdvanceImage Value { get; set; }
        public string Path { get; set; }
        public List<Adapter.AdvanceImage.AdvanceImageRect> Splits { get; set; }
        public List<Adapter.AdvanceImage.AdvanceImageRect> ReSplits { get; set; }
        public SingleImageChoser ()
        {
            InitializeComponent();
            this.Path = "";
        }

        private void btOK_Click (object sender, EventArgs e)
        {
            if ( numericUpDown1.Focused ) return;
            var r = protoImageDisplayer1.Rect;
            Value.ImageIndex.Value = r[r.X, r.Y];
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btCancel_Click (object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void numericUpDown1_ValueChanged (object sender, EventArgs e)
        {
            protoImageDisplayer1.Rect.SetIndex((int)numericUpDown1.Value);
            protoImageDisplayer1.Invalidate();
        }

        private void SingleImageChoser_Shown (object sender, EventArgs e)
        {
            string name = Help.RtpManager.SearchFile(Path);
            if ( name == null || name == null || Value == null ) Close();
            Bitmap bitmap = new Bitmap(name);
            var r = Splits[0];
            if ( ReSplits != null )
                foreach ( var rect in ReSplits )
                    if ( rect.Flag != "" && name.StartsWith(rect.Flag) ) { name = name.Remove(0, rect.Flag.Length); break; }
            foreach ( var rect in Splits )
                if ( rect.Flag != "" && name.StartsWith(rect.Flag) )
                    r = rect;
            r.SetIndex((int)Value.ImageIndex.Value);
            protoImageDisplayer1.Rect = r;
            protoImageDisplayer1.ClientSize = bitmap.Size;
            protoImageDisplayer1.Bitmap = bitmap;
            protoImageDisplayer1.Invalidate();
            label1.Text = "Index : " + protoImageDisplayer1.Index;
            numericUpDown1.Maximum = r.Blocks - 1;
            numericUpDown1.Value = (int)Value.ImageIndex.Value;
        }

        private void protoImageDisplayer1_MouseDoubleClick (object sender, MouseEventArgs e)
        {
            btOK_Click(btOK, e);
        }

        private void protoImageDisplayer1_SelectedIndexChanged (object sender, EventArgs e)
        {
            label1.Text = "Index : " + protoImageDisplayer1.Index;
            var r = protoImageDisplayer1.Rect;
            numericUpDown1.Value = r[r.X, r.Y];
        }

        private void label1_Click (object sender, EventArgs e)
        {
            if ( label1.Width > 100 )
            {
                label1.Width = 94;
                label1.Text = "/ " + protoImageDisplayer1.Rect.Blocks;
                numericUpDown1.Visible = true;
                numericUpDown1.Focus();
            }
            else { protoImageDisplayer1.Focus(); }
        }

        private void numericUpDown1_Leave (object sender, EventArgs e)
        {
            label1.Width = 191;
            label1.Text = "Index : " + protoImageDisplayer1.Index;
            numericUpDown1.Visible = false;
        }

        private void numericUpDown1_KeyUp (object sender, KeyEventArgs e)
        {
            if ( e.KeyCode == Keys.Enter ) protoImageDisplayer1.Focus();
        }
    }
}
