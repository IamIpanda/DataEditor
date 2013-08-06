using System;
using System.Collections.Generic;
using System.Text;
using DataEditor.Control.Prototype;
using System.Windows.Forms;
using System.Drawing;

namespace DataEditor.Control.Wrapper
{
    public class WrapImageDisplayer : ProtoImageBackgroundDisplayer
    {
        ProtoImageControlHelp.ProtoImageSplit split = null;
        Pen pen;
        public int Index { get; set; }
        public string ImageName { get; set; }
        public WrapImageDisplayer()
        {
            this.pen = new Pen(Color.White, 2F);
            this.ImageName = " ";
            InitializeComponent();
            
        }
        public void Set(string split)
        {
            this.split = new ProtoImageControlHelp.ProtoImageSplit(split);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (this.split == null) return;
            Rectangle rect = split[base.bitmap, ImageName, this.Index];
            e.Graphics.DrawRectangle(pen, rect);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // WrapImageDisplayer
            // 
            this.Name = "WrapImageDisplayer";
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.WrapImageDisplayer_MouseClick);
            this.ResumeLayout(false);

        }

        private void WrapImageDisplayer_MouseClick(object sender, MouseEventArgs e)
        {
            if (base.bitmap == null) return;
            if (split == null) return;
            Size size = split[base.bitmap, ImageName];
            int maxx = base.bitmap.Width / size.Width;
            int maxy = base.bitmap.Height / size.Height;
            int x = e.X / size.Width;
            int y = e.Y / size.Height;
            if (x >= maxx || y >= maxy) return;
            this.Index = y * maxx + x;
            Invalidate();
        }

        public int Blocks
        {
            get 
            {
                if (base.bitmap == null) return 0;
                if (split == null) return 0; 
                Size size = split[base.bitmap, ImageName];
                int maxx = base.bitmap.Width / size.Width;
                int maxy = base.bitmap.Height / size.Height;
                return maxx * maxy;
            }
        }
    }
}
