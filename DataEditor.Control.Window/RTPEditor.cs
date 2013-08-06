using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DataEditor.Help;

namespace DataEditor.Control.Window
{
    public partial class RTPEditor : Form
    {
        List<Rtp> EditingRtps = new List<Rtp>();
        Rtp? ViewingRtp = null;

        public RTPEditor()
        {
            InitializeComponent();
        }

        private void RtpInnerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.IO.FileInfo file = RtpInnerList.ChosenFile;
            // === File Show ====
            if (ViewingRtp == null)
                return;
            string filestring = file == null ?
                (RtpInnerList.ChosenDirectory == null ? "" : RtpInnerList.ChosenDirectory.FullName) :
                file.FullName;
            string s = new System.IO.DirectoryInfo(ViewingRtp.Value.Path).FullName;
            filestring = filestring.Replace(s, ViewingRtp.Value.ToString());
            Strtb.Text = filestring;
            //===================
            if (file == null)
                return;
            Bitmap b;
            try
            {
                b = new Bitmap(file.FullName);
            }
            catch { return; }
            RtpImageDisplayer.Bitmap = b;
            RtpImageDisplayer.Invalidate();
            SizeTb.Text = "(" + b.Width.ToString() + "," + b.Height.ToString() + ")";
        }


        private void RtpInnerList_DoubleClick(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = RtpInnerList.ChosenDirectory;
            if (dir == null)
                return;
            if (ViewingRtp == null)
                return;
            RtpInnerList.Clear();
            RtpInnerList.Items.Clear();
            RtpInnerList.AddChildDirectory(dir, ViewingRtp.Value.Color, !(dir.FullName == ViewingRtp.Value.Path));
            RtpInnerList.AddChildFile(dir, ViewingRtp.Value.Color);
        }
        private void RtpList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = RtpList.SelectedIndex;
            if (index >= RtpList.Items.Count || index < 0)
                return;
            Rtp rtp = (Rtp)RtpList.Items[index];
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(rtp.Path);
            if (!dir.Exists)
                return;
            RtpInnerList.Clear();
            RtpInnerList.Items.Clear();
            RtpInnerList.AddChildDirectory(dir, rtp.Color, false);
            RtpInnerList.AddChildFile(dir, rtp.Color);
            ViewingRtp = rtp;
            // ==== 移动处理 ====
            btUp.Enabled = !(RtpList.SelectedIndex == 0);
            btDown.Enabled = !(RtpList.SelectedIndex >= EditingRtps.Count - 1);
            btDel.Enabled = !rtp.IsFromReg;
            // ==== 赋予处理 ====
            Strtb.Text = rtp.FullString();
            cbNewRtp.Text = rtp.Path;
            lbColor.BackColor = rtp.Color;
            // ==================
        }

        private void RTPEditor_Shown(object sender, EventArgs e)
        {
            EditingRtps = RtpManager.Rtp;
            LoadRtps();
            if (EditingRtps.Count > 0)
            {
                RtpList.SelectedIndex = 0;
                RtpList.Focus();
            }
            Refresh();
        }

        protected void LoadRtps()
        {
            RtpList.Items.Clear();
            RtpList.Color.ActualValue.Clear();
            foreach (Rtp rtp in EditingRtps)
            {
                RtpList.Items.Add(rtp);
                RtpList.Color.ActualValue.Add(rtp.Color);
                cbNewRtp.Items.Add(rtp.FullString());
            }
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            RtpManager.Rtp = EditingRtps;
            this.Close();
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (FolderBD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                cbNewRtp.Focus();
                cbNewRtp.Text = FolderBD.SelectedPath;
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void btDel_Click(object sender, EventArgs e)
        {
            if (RtpList.SelectedIndex < 0) return;
            EditingRtps.RemoveAt(RtpList.SelectedIndex);
            RtpList.Color.ActualValue.RemoveAt(RtpList.SelectedIndex);
            int OriginIndex = RtpList.SelectedIndex;
            int ChangeIndex = OriginIndex - 1;
            if (ChangeIndex < 0) ChangeIndex = 0;
            RtpList.SelectedIndex = ChangeIndex;
            RtpList.Items.RemoveAt(OriginIndex);
        }

        private void btUp_Click(object sender, EventArgs e)
        {
            if (RtpList.SelectedIndex < 0) return;
            EditingRtps.Reverse(RtpList.SelectedIndex - 1, 2);
            RtpList.Color.ActualValue.Reverse(RtpList.SelectedIndex - 1, 2);
            object temp = RtpList.Items[RtpList.SelectedIndex - 1];
            RtpList.Items[RtpList.SelectedIndex - 1] = RtpList.Items[RtpList.SelectedIndex];
            RtpList.Items[RtpList.SelectedIndex] = temp;
            RtpList.SelectedIndex -= 1;
        }

        private void btDown_Click(object sender, EventArgs e)
        {
            if (RtpList.SelectedIndex < 0) return;
            EditingRtps.Reverse(RtpList.SelectedIndex, 2);
            RtpList.Color.ActualValue.Reverse(RtpList.SelectedIndex, 2);
            object temp = RtpList.Items[RtpList.SelectedIndex + 1];
            RtpList.Items[RtpList.SelectedIndex + 1] = RtpList.Items[RtpList.SelectedIndex];
            RtpList.Items[RtpList.SelectedIndex] = temp;
            RtpList.SelectedIndex += 1;
        }

        private void cbNewRtp_Enter(object sender, EventArgs e)
        {
            cbNewRtp.Text = "";
            cbNewRtp.ForeColor = Color.Black;
        }

        private void cbNewRtp_Leave(object sender, EventArgs e)
        {
            cbNewRtp.Text = "要添加 RTP，请于此键入...";
            cbNewRtp.ForeColor = Color.Silver;
        }

        private void cbNewRtp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Rtp rtp;
                try 
                {
                    rtp = Rtp.FromString(cbNewRtp.Text, lbColor.BackColor);
                    if (!new System.IO.DirectoryInfo(rtp.Path).Exists) return;
                    cbNewRtp.Text = "";
                    EditingRtps.Add(rtp);
                    RtpList.Items.Add(rtp);
                    RtpList.Color.ActualValue.Add(rtp.Color);
                    RtpList.SelectedIndex = RtpList.Items.Count - 1;
                    RtpList.Focus();
                }
                catch { return; }
            }
            else if (e.KeyChar == 26)
            {
                cbNewRtp.Text = "";
                lbColor.BackColor = Color.Gray;
            }
        }

        private void lbColor_Click(object sender, EventArgs e)
        {
            if (colorD.ShowDialog() == DialogResult.OK)
                lbColor.BackColor = colorD.Color;
        }
        
    }
}
