namespace DataEditor.Control.Window
{
    partial class ArrayChangeDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose (bool disposing)
        {
            if ( disposing && (components != null) )
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
        private void InitializeComponent ()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbmain = new DataEditor.Control.Prototype.ProtoComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.nudmax = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.nudmin = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.tbmain = new System.Windows.Forms.TextBox();
            this.lbmain = new DataEditor.Control.Prototype.ProtoListBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudmax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudmin)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.28915F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.71085F));
            this.tableLayoutPanel1.Controls.Add(this.button1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.button2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbmain, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbmain, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(608, 509);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.Location = new System.Drawing.Point(442, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(163, 20);
            this.button1.TabIndex = 1;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button2.Location = new System.Drawing.Point(442, 29);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(163, 20);
            this.button2.TabIndex = 2;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(442, 55);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(163, 451);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cbmain);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.nudmax);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.nudmin);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 29);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(433, 20);
            this.panel2.TabIndex = 5;
            // 
            // cbmain
            // 
            this.cbmain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbmain.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbmain.FormattingEnabled = true;
            this.cbmain.ItemHeight = 12;
            this.cbmain.Location = new System.Drawing.Point(266, 0);
            this.cbmain.Name = "cbmain";
            this.cbmain.Size = new System.Drawing.Size(167, 18);
            this.cbmain.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(210, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "号对象的";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nudmax
            // 
            this.nudmax.Dock = System.Windows.Forms.DockStyle.Left;
            this.nudmax.Location = new System.Drawing.Point(146, 0);
            this.nudmax.Name = "nudmax";
            this.nudmax.Size = new System.Drawing.Size(64, 21);
            this.nudmax.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(129, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "～";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nudmin
            // 
            this.nudmin.Dock = System.Windows.Forms.DockStyle.Left;
            this.nudmin.Location = new System.Drawing.Point(65, 0);
            this.nudmin.Name = "nudmin";
            this.nudmin.Size = new System.Drawing.Size(64, 21);
            this.nudmin.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "一并更改：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbmain
            // 
            this.tbmain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbmain.Location = new System.Drawing.Point(3, 3);
            this.tbmain.Name = "tbmain";
            this.tbmain.Size = new System.Drawing.Size(433, 21);
            this.tbmain.TabIndex = 6;
            this.tbmain.TextChanged += new System.EventHandler(this.tbmain_TextChanged);
            // 
            // lbmain
            // 
            this.lbmain.DisappearRectLosingFocus = true;
            this.lbmain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbmain.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lbmain.FormattingEnabled = true;
            this.lbmain.Location = new System.Drawing.Point(3, 55);
            this.lbmain.Name = "lbmain";
            this.lbmain.Size = new System.Drawing.Size(433, 451);
            this.lbmain.TabIndex = 7;
            // 
            // ArrayChangeDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 509);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ArrayChangeDialog";
            this.ShowInTaskbar = false;
            this.Text = "一并更改";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudmax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudmin)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox tbmain;
        private Prototype.ProtoListBox lbmain;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudmax;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudmin;
        private Prototype.ProtoComboBox cbmain;

    }
}