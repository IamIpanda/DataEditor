namespace DataEditor.Control.ProtoType.Any
{
    partial class AnyTypeEditor
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("");
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeViewForAny1 = new DataEditor.Control.ProtoType.Any.TreeViewForAny();
            this.listViewForAny1 = new DataEditor.Control.ProtoType.Any.ListViewForAny();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeViewForAny1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listViewForAny1);
            this.splitContainer1.Size = new System.Drawing.Size(699, 433);
            this.splitContainer1.SplitterDistance = 148;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeViewForAny1
            // 
            this.treeViewForAny1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewForAny1.FullRowSelect = true;
            this.treeViewForAny1.HideSelection = false;
            this.treeViewForAny1.Location = new System.Drawing.Point(0, 0);
            this.treeViewForAny1.Name = "treeViewForAny1";
            treeNode1.Name = "";
            treeNode1.Text = "";
            this.treeViewForAny1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.treeViewForAny1.Size = new System.Drawing.Size(148, 433);
            this.treeViewForAny1.TabIndex = 0;
            this.treeViewForAny1.Value = null;
            this.treeViewForAny1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewForAny1_AfterSelect);
            // 
            // listViewForAny1
            // 
            this.listViewForAny1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listViewForAny1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewForAny1.FullRowSelect = true;
            this.listViewForAny1.Location = new System.Drawing.Point(0, 0);
            this.listViewForAny1.Name = "listViewForAny1";
            this.listViewForAny1.Size = new System.Drawing.Size(547, 433);
            this.listViewForAny1.TabIndex = 0;
            this.listViewForAny1.UseCompatibleStateImageBehavior = false;
            this.listViewForAny1.Value = null;
            this.listViewForAny1.View = System.Windows.Forms.View.Details;
            this.listViewForAny1.DoubleClick += new System.EventHandler(this.listViewForAny1_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "索引";
            this.columnHeader1.Width = 102;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "类型";
            this.columnHeader2.Width = 68;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "值";
            this.columnHeader3.Width = 350;
            // 
            // AnyTypeEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "AnyTypeEditor";
            this.Size = new System.Drawing.Size(699, 433);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private TreeViewForAny treeViewForAny1;
        private ListViewForAny listViewForAny1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
    }
}
