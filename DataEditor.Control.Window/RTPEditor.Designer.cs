namespace DataEditor.Control.Window
{
    partial class RTPEditor
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RTPEditor));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.RtpImageDisplayer = new DataEditor.Control.Prototype.ProtoImageBackgroundDisplayer();
            this.RtpInnerList = new DataEditor.Control.Prototype.ProtoRtpViewList();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.RtpList = new DataEditor.Control.Prototype.ProtoColoredListBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btDel = new System.Windows.Forms.Button();
            this.btDown = new System.Windows.Forms.Button();
            this.btUp = new System.Windows.Forms.Button();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.lbColor = new System.Windows.Forms.Label();
            this.btAdd = new System.Windows.Forms.Button();
            this.cbNewRtp = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.Strtb = new System.Windows.Forms.TextBox();
            this.btCancel = new System.Windows.Forms.Button();
            this.btOK = new System.Windows.Forms.Button();
            this.SizeTb = new System.Windows.Forms.TextBox();
            this.colorD = new System.Windows.Forms.ColorDialog();
            this.FolderBD = new System.Windows.Forms.FolderBrowserDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.RtpImageDisplayer, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.RtpInnerList, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 2, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // RtpImageDisplayer
            // 
            this.RtpImageDisplayer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this.RtpImageDisplayer.Bitmap = null;
            this.RtpImageDisplayer.BlockHeight = 12;
            this.RtpImageDisplayer.BlockWidth = 12;
            resources.ApplyResources(this.RtpImageDisplayer, "RtpImageDisplayer");
            this.RtpImageDisplayer.ImageAlignCenter = true;
            this.RtpImageDisplayer.Name = "RtpImageDisplayer";
            this.RtpImageDisplayer.Scale = false;
            this.RtpImageDisplayer.SrcRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // RtpInnerList
            // 
            this.RtpInnerList.DisappearRectLosingFocus = true;
            resources.ApplyResources(this.RtpInnerList, "RtpInnerList");
            this.RtpInnerList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.RtpInnerList.FormattingEnabled = true;
            this.RtpInnerList.Name = "RtpInnerList";
            this.tableLayoutPanel1.SetRowSpan(this.RtpInnerList, 2);
            this.RtpInnerList.SelectedIndexChanged += new System.EventHandler(this.RtpInnerList_SelectedIndexChanged);
            this.RtpInnerList.DoubleClick += new System.EventHandler(this.RtpInnerList_DoubleClick);
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.RtpList, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel5, 0, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel1.SetRowSpan(this.tableLayoutPanel2, 2);
            // 
            // RtpList
            // 
            this.RtpList.DisappearRectLosingFocus = false;
            resources.ApplyResources(this.RtpList, "RtpList");
            this.RtpList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.RtpList.FormattingEnabled = true;
            this.RtpList.Name = "RtpList";
            this.RtpList.SelectedIndexChanged += new System.EventHandler(this.RtpList_SelectedIndexChanged);
            // 
            // tableLayoutPanel4
            // 
            resources.ApplyResources(this.tableLayoutPanel4, "tableLayoutPanel4");
            this.tableLayoutPanel4.Controls.Add(this.btDel, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.btDown, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.btUp, 0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            // 
            // btDel
            // 
            resources.ApplyResources(this.btDel, "btDel");
            this.btDel.Name = "btDel";
            this.btDel.UseVisualStyleBackColor = true;
            this.btDel.Click += new System.EventHandler(this.btDel_Click);
            // 
            // btDown
            // 
            resources.ApplyResources(this.btDown, "btDown");
            this.btDown.Name = "btDown";
            this.btDown.UseVisualStyleBackColor = true;
            this.btDown.Click += new System.EventHandler(this.btDown_Click);
            // 
            // btUp
            // 
            resources.ApplyResources(this.btUp, "btUp");
            this.btUp.Name = "btUp";
            this.btUp.UseVisualStyleBackColor = true;
            this.btUp.Click += new System.EventHandler(this.btUp_Click);
            // 
            // tableLayoutPanel5
            // 
            resources.ApplyResources(this.tableLayoutPanel5, "tableLayoutPanel5");
            this.tableLayoutPanel5.Controls.Add(this.lbColor, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.btAdd, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.cbNewRtp, 0, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            // 
            // lbColor
            // 
            this.lbColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbColor.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.lbColor, "lbColor");
            this.lbColor.Name = "lbColor";
            this.lbColor.Click += new System.EventHandler(this.lbColor_Click);
            // 
            // btAdd
            // 
            resources.ApplyResources(this.btAdd, "btAdd");
            this.btAdd.Name = "btAdd";
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // cbNewRtp
            // 
            resources.ApplyResources(this.cbNewRtp, "cbNewRtp");
            this.cbNewRtp.ForeColor = System.Drawing.Color.Silver;
            this.cbNewRtp.FormattingEnabled = true;
            this.cbNewRtp.Name = "cbNewRtp";
            this.cbNewRtp.Enter += new System.EventHandler(this.cbNewRtp_Enter);
            this.cbNewRtp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbNewRtp_KeyPress);
            this.cbNewRtp.Leave += new System.EventHandler(this.cbNewRtp_Leave);
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.Strtb, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.btCancel, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.btOK, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.SizeTb, 1, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // Strtb
            // 
            this.Strtb.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.Strtb, "Strtb");
            this.Strtb.Name = "Strtb";
            this.Strtb.ReadOnly = true;
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btCancel, "btCancel");
            this.btCancel.Name = "btCancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btOK
            // 
            resources.ApplyResources(this.btOK, "btOK");
            this.btOK.Name = "btOK";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // SizeTb
            // 
            this.SizeTb.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.SizeTb, "SizeTb");
            this.SizeTb.Name = "SizeTb";
            this.SizeTb.ReadOnly = true;
            // 
            // FolderBD
            // 
            resources.ApplyResources(this.FolderBD, "FolderBD");
            // 
            // RTPEditor
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RTPEditor";
            this.Shown += new System.EventHandler(this.RTPEditor_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Prototype.ProtoImageBackgroundDisplayer RtpImageDisplayer;
        private Prototype.ProtoRtpViewList RtpInnerList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private Prototype.ProtoColoredListBox RtpList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button btDel;
        private System.Windows.Forms.Button btDown;
        private System.Windows.Forms.Button btUp;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label lbColor;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.ComboBox cbNewRtp;
        private System.Windows.Forms.ColorDialog colorD;
        private System.Windows.Forms.FolderBrowserDialog FolderBD;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.TextBox SizeTb;
        private System.Windows.Forms.TextBox Strtb;
    }
}

