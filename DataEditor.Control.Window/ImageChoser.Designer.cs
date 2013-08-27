namespace DataEditor.Control.Window
{
    partial class ImageChoser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageChoser));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.TitleBox = new DataEditor.Control.Prototype.ProtoTitleBox();
            this.RTPChoser = new DataEditor.Control.Prototype.ProtoComboBox();
            this.fileList = new DataEditor.Control.Prototype.ProtoRtpViewList();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btOK = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.btIndex = new System.Windows.Forms.TextBox();
            this.wrapImageDisplayer1 = new DataEditor.Control.Prototype.ProtoImageIndexDisplayer();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.87593F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 81.12407F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 517F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(943, 517);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.TitleBox, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.RTPChoser, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.fileList, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(172, 511);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // TitleBox
            // 
            this.TitleBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TitleBox.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TitleBox.ForeColor = System.Drawing.Color.White;
            this.TitleBox.Location = new System.Drawing.Point(7, 7);
            this.TitleBox.Margin = new System.Windows.Forms.Padding(7);
            this.TitleBox.Name = "TitleBox";
            this.TitleBox.Size = new System.Drawing.Size(158, 50);
            this.TitleBox.TabIndex = 0;
            // 
            // RTPChoser
            // 
            this.RTPChoser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RTPChoser.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.RTPChoser.FormattingEnabled = true;
            this.RTPChoser.ItemHeight = 12;
            this.RTPChoser.Location = new System.Drawing.Point(3, 67);
            this.RTPChoser.Name = "RTPChoser";
            this.RTPChoser.Size = new System.Drawing.Size(166, 18);
            this.RTPChoser.TabIndex = 2;
            this.RTPChoser.SelectedIndexChanged += new System.EventHandler(this.RTPChoser_SelectedIndexChanged);
            // 
            // fileList
            // 
            this.fileList.DisappearRectLosingFocus = false;
            this.fileList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.fileList.FormattingEnabled = true;
            this.fileList.Location = new System.Drawing.Point(3, 91);
            this.fileList.Name = "fileList";
            this.fileList.Size = new System.Drawing.Size(166, 417);
            this.fileList.TabIndex = 3;
            this.fileList.SelectedIndexChanged += new System.EventHandler(this.fileList_SelectedIndexChanged);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.wrapImageDisplayer1, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(181, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(759, 511);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 4;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 123F));
            this.tableLayoutPanel4.Controls.Add(this.btOK, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.btCancel, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.btIndex, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 482);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(753, 26);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // btOK
            // 
            this.btOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btOK.Location = new System.Drawing.Point(513, 3);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(114, 20);
            this.btOK.TabIndex = 0;
            this.btOK.Text = "确定";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btCancel.Location = new System.Drawing.Point(633, 3);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(117, 20);
            this.btCancel.TabIndex = 1;
            this.btCancel.Text = "取消";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btIndex
            // 
            this.btIndex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btIndex.Location = new System.Drawing.Point(258, 3);
            this.btIndex.Name = "btIndex";
            this.btIndex.ReadOnly = true;
            this.btIndex.Size = new System.Drawing.Size(249, 21);
            this.btIndex.TabIndex = 2;
            this.btIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // wrapImageDisplayer1
            // 
            this.wrapImageDisplayer1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.wrapImageDisplayer1.BackColors = ((System.Collections.Generic.List<System.Drawing.Color>)(resources.GetObject("wrapImageDisplayer1.BackColors")));
            this.wrapImageDisplayer1.Bitmap = null;
            this.wrapImageDisplayer1.BlockHeight = 12;
            this.wrapImageDisplayer1.BlockWidth = 12;
            this.wrapImageDisplayer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wrapImageDisplayer1.FullBackgroundDraw = false;
            this.wrapImageDisplayer1.ImageAlignCenter = false;
            this.wrapImageDisplayer1.Index = 0;
            this.wrapImageDisplayer1.Location = new System.Drawing.Point(3, 3);
            this.wrapImageDisplayer1.Name = "wrapImageDisplayer1";
            this.wrapImageDisplayer1.Rect = null;
            this.wrapImageDisplayer1.Scale = false;
            this.wrapImageDisplayer1.Size = new System.Drawing.Size(753, 473);
            this.wrapImageDisplayer1.SrcRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.wrapImageDisplayer1.TabIndex = 1;
            this.wrapImageDisplayer1.UseRectangleFocus = false;
            this.wrapImageDisplayer1.Value = null;
            this.wrapImageDisplayer1.SelectedIndexChanged += new System.EventHandler<System.EventArgs>(this.wrapImageDisplayer1_SelectedIndexChanged);
            this.wrapImageDisplayer1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.wrapImageDisplayer1_MouseDoubleClick);
            // 
            // ImageChoser
            // 
            this.AcceptButton = this.btOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(943, 517);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImageChoser";
            this.Text = "选取图片";
            this.Shown += new System.EventHandler(this.ImageChoser_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Prototype.ProtoTitleBox TitleBox;
        private Prototype.ProtoComboBox RTPChoser;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.TextBox btIndex;
        private Prototype.ProtoImageIndexDisplayer wrapImageDisplayer1;
        private Prototype.ProtoRtpViewList fileList;
    }
}