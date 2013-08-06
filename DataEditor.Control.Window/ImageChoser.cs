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
    public partial class ImageChoser : Form
    {
        static string[] miss = new string[] { "!" };
        protected string path = "", filename = "";
        public string Path 
        {
            get { return path; }
            set { path = value; }
        }
        public string FileName
        {
            get
            {
                if (fileList.Items.Count == 0) return "";
                System.IO.FileInfo file = fileList.ChosenFile;
                if (file == null) return "";
                return System.IO.Path.GetFileNameWithoutExtension(file.FullName);
            }
            set
            {
                if (fileList.Items.Count == 0) return;
                foreach (object ob in fileList.Items)
                    if (ob.ToString() == value)
                        fileList.SelectedItem = ob;
            }
        }
        public int Index 
        {
            get { return wrapImageDisplayer1.Index; }
            set { wrapImageDisplayer1.Index = value; }
        }
        public string Split
        {
            set { wrapImageDisplayer1.Set(value); }
        }
        public ImageChoser()
        {
            InitializeComponent();
        }
        public new string Text
        {
            get { return TitleBox.Text; }
            set { TitleBox.Text = value; }
        }

        protected void InitializeRTP()
        {
            RTPChoser.Items.Clear();
            RTPChoser.Items.Add("全部");
            foreach (Rtp rtp in RtpManager.Rtp)
                RTPChoser.Items.Add(rtp);
            RTPChoser.SelectedIndex = 0;
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

        private void ImageChoser_Shown(object sender, EventArgs e)
        {
            InitializeRTP();
        }

        private void RTPChoser_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetListbox();
        }

        private void fileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fileList.SelectedIndex == 0)
            {
                wrapImageDisplayer1.Bitmap = null;
                return;
            }
            System.IO.FileInfo file = fileList.ChosenFile;
            if (file == null) return;
            if (!file.Exists) return;
            Bitmap bitmap = new Bitmap(file.FullName);
            string name = file.Name;
            foreach (string pass in miss)
                if (name.StartsWith(pass))
                    name = name.Remove(0, pass.Length);
            wrapImageDisplayer1.ImageName = name;
            SetBitmap(bitmap);
            SetText();
        }

        private void wrapImageDisplayer1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btOK_Click(sender, e);
        }

        private void wrapImageDisplayer1_MouseUp(object sender, MouseEventArgs e)
        {
            SetText();
        }
        protected void SearchFile(Rtp rtp, string path)
        {
            string full = System.IO.Path.Combine(rtp.Path, path);
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(full);
            if (!dir.Exists) return;
            fileList.AddChildFile(dir, rtp.Color);
        }
        protected void SetListbox()
        {
            fileList.Items.Clear();
            fileList.Clear();
            fileList.AddFlag("（无）",Color.Black);
            int index = RTPChoser.SelectedIndex;
            if (index == -1) return;
            else if (index == 0)
                foreach (Rtp rtp in RtpManager.Rtp)
                    SearchFile(rtp, path);
            else
                SearchFile(RtpManager.Rtp[index - 1], path);
        }
        protected void SetBitmap(Bitmap bitmap)
        {
            wrapImageDisplayer1.Bitmap = bitmap;
            wrapImageDisplayer1.Index = 0;
        }

        protected void SetText()
        {
            btIndex.Text = FileName + " ( " + Index + " / " + wrapImageDisplayer1.Blocks + " ) ";
        }
    }
}