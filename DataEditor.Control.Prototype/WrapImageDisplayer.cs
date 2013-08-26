using System;
using System.Collections.Generic;
using System.Text;
using DataEditor.Control.Prototype;
using System.Windows.Forms;
using System.Drawing;

namespace DataEditor.Control.Wrapper
{
    // TODO : Rebuild it
    public class WrapImageDisplayer : ProtoImageBackgroundDisplayer
    {
        ProtoImageControlHelp.ProtoImageSplit split = null;
        Pen WhitePen, BlackPen;
        // ============= Banish ===========
        public int Index { get; set; }
        public string ImageName { get; set; }
        // ==============================
        public Adapter.AdvanceImage.AdvanceImageRect Rect { get; set; }
        public Adapter.AdvanceImage Value { get; set; }
       
        public WrapImageDisplayer()
        {
            this.WhitePen = new Pen(Color.White, 2F);
            this.BlackPen = new Pen(Color.Black, 0.2F);
            this.ImageName = " ";
            InitializeComponent();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if ( bitmap == null || Rect == null ) return;
            Rectangle r = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            Rectangle ans = Rect.Split(r);
            if ( Blocks > 1 ) e.Graphics.DrawRectangle(WhitePen, ans.X + 1, ans.Y + 1, ans.Width - 2.5F, ans.Height - 2.5F);
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
            if ( Rect == null ) return;
            Rect.SetIndex(e.X, e.Y);
            this.Index = Rect[Rect.X, Rect.Y];
            Invalidate();
        }

        public int Blocks
        {
            get 
            {
                if (base.bitmap == null) return 0;
                if ( Rect == null ) return 0;
                return Rect.Blocks;
            }
        }
    }
}
