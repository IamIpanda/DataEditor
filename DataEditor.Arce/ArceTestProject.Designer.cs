﻿namespace DataEditor.Arce
{
    partial class ArceTestProject
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
            this.protoDropItem1 = new DataEditor.Control.Prototype.ProtoDropItem();
            this.SuspendLayout();
            // 
            // protoDropItem1
            // 
            this.protoDropItem1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.protoDropItem1.Location = new System.Drawing.Point(207, 90);
            this.protoDropItem1.MaximumSize = new System.Drawing.Size(99999, 22);
            this.protoDropItem1.MinimumSize = new System.Drawing.Size(2, 22);
            this.protoDropItem1.Name = "protoDropItem1";
            this.protoDropItem1.Size = new System.Drawing.Size(451, 22);
            this.protoDropItem1.TabIndex = 0;
            // 
            // ArceTestProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 499);
            this.Controls.Add(this.protoDropItem1);
            this.Name = "ArceTestProject";
            this.Text = "DataEditor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Control.Prototype.ProtoDropItem protoDropItem1;
















    }
}

