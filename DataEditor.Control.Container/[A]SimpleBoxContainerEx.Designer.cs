namespace DataEditor.Control.Container
{
    partial class SimpleBoxContainerEx
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lbName = new System.Windows.Forms.Label();
            this.simpleContainer1 = new DataEditor.Control.Container.SimpleContainer();
            this.SuspendLayout();
            // 
            // lbName
            // 
            this.lbName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbName.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbName.Location = new System.Drawing.Point(0, 0);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(389, 16);
            this.lbName.TabIndex = 0;
            this.lbName.Text = "label1";
            this.lbName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // simpleContainer1
            // 
            this.simpleContainer1.Arguments = null;
            this.simpleContainer1.Label = null;
            this.simpleContainer1.Location = new System.Drawing.Point(0, 16);
            this.simpleContainer1.Margin = new System.Windows.Forms.Padding(12, 12, 12, 3);
            this.simpleContainer1.Name = "simpleContainer1";
            this.simpleContainer1.Size = new System.Drawing.Size(389, 312);
            this.simpleContainer1.TabIndex = 1;
            this.simpleContainer1.Text = "simpleContainer1";
            this.simpleContainer1.BackColorChanged += new System.EventHandler(this.simpleContainer1_BackColorChanged);
            // 
            // SimpleBoxContainerEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.simpleContainer1);
            this.Controls.Add(this.lbName);
            this.Name = "SimpleBoxContainerEx";
            this.Size = new System.Drawing.Size(389, 328);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbName;
        private SimpleContainer simpleContainer1;
    }
}
