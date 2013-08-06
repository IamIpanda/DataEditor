using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace DataEditor.Control.Prototype
{
    public class ProtoImageDisplayer : UserControl, ProtoImageControl, ProtoFrameControl
    {
        protected Bitmap bitmap = null;
        protected Rectangle src_rect = new Rectangle(0, 0, 0, 0);

        public new bool Scale { get; set; }
        public bool ImageAlignCenter { get; set; }
        public bool UseRectangleFocus { get; set; }
        
        public Bitmap Bitmap 
        {
            get { return bitmap; }
            set { bitmap = value; Invalidate(); }
        }
        
        public Rectangle SrcRect
        {
            get { return src_rect; }
            set { src_rect = value; Invalidate(); }
        }

        public ProtoImageDisplayer()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.Scale = true;
            this.ImageAlignCenter = false;
            this.UseRectangleFocus = true;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ProtoImageDisplayer
            // 
            this.Name = "ProtoImageDisplayer";
            this.Enter += new System.EventHandler(this.ProtoImageDisplayer_Enter);
            this.Leave += new System.EventHandler(this.ProtoImageDisplayer_Leave);
            this.ResumeLayout(false);

        }

        private void ProtoImageDisplayer_Enter(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void ProtoImageDisplayer_Leave(object sender, EventArgs e)
        {
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DrawBackGround(e.Graphics);
            ProtoFrameControlHelp.DrawBorder(e.Graphics, this.ClientRectangle, Color.Black);
            if (Bitmap == null)
                return;
            Rectangle rect = SrcRect;
            if (SrcRect.Width == 0 && SrcRect.Height == 0)
                rect = new Rectangle(new Point(0, 0), bitmap.Size);
            int x = 0, y = 0;
            if (ImageAlignCenter)
            {
                x = (int)(e.Graphics.ClipBounds.Width - rect.Width) / 2;
                y = (int)(e.Graphics.ClipBounds.Height - rect.Height) / 2;
            }
            else 
            {
                x = ((bitmap.Width / 32 * 32) - bitmap.Width) / 2;
                y = ((bitmap.Height / 32 * 32) - bitmap.Height) / 2;
            }
            if (Scale)
                e.Graphics.DrawImage(bitmap, e.ClipRectangle, rect, GraphicsUnit.Pixel);
            else
                e.Graphics.DrawImage(bitmap, new Rectangle(0, 0, rect.Width, rect.Height), rect, GraphicsUnit.Pixel);
            if (this.UseRectangleFocus && this.Focused)
                ProtoFrameControlHelp.DrawFocusRectangle(e.Graphics, e.ClipRectangle);
        }

        protected virtual void DrawBackGround(Graphics graphics)
        {
            graphics.Clear(Color.White);
        }
    }
    public class ProtoImageBackgroundDisplayer : ProtoImageDisplayer
    {
        static protected List<Color> BackColors = new List<Color>()
        {
            Color.FromArgb(0, 0, 128),
            Color.FromArgb(0, 0, 65)
        };

        public int BlockWidth { get; set; }
        public int BlockHeight { get; set; }

        public ProtoImageBackgroundDisplayer()
            : base()
        {
            this.BlockWidth = 12;
            this.BlockHeight = 12;
        }
        static ProtoImageBackgroundDisplayer()
        {
            LoadSettings();
        }
        static public void SaveSettings()
        {
            DataEditor.Help.InitalizedObjectEditorHelper.SetOption(typeof(ProtoImageBackgroundDisplayer), BackColors);
        }
        static public void LoadSettings()
        {
            try
            {
                BackColors = DataEditor.Help.InitalizedObjectEditorHelper.GetOption
                    (typeof(ProtoImageBackgroundDisplayer)) as List<Color>;
                if (BackColors == null)
                    BackColors = new List<Color>() { DefaultBackColor };
            }
            catch { }
        }
        protected override void DrawBackGround(Graphics graphics)
        {
            graphics.Clear(BackColor);
            if (bitmap == null)
                return;
            int w, h;
            if (src_rect.Width == 0 && src_rect.Height == 0)
            { w = bitmap.Width; h = bitmap.Height; }
            else
            { w = src_rect.Width; h = src_rect.Height; }
            DrawRectangeleBackGround(graphics, new Rectangle(0, 0, w, h));
        }
        protected void DrawRectangeleBackGround(Graphics graphics, Rectangle rect)
        {
            if (BackColors.Count == 0)
                graphics.FillRectangle(new SolidBrush(BackColor), graphics.ClipBounds);
            else if (BackColors.Count == 1)
                graphics.FillRectangle(new SolidBrush(BackColors[0]), graphics.ClipBounds);
            else
            {
                int Length = BackColors.Count;
                int X = rect.X;
                int Y = rect.Y;
                int StartX = X;
                int CountX = 0, CountY = 0;
                int Right = rect.Right;
                int Bottom = rect.Bottom;
                List<Brush> Brushes = new List<Brush>();
                foreach (Color c in BackColors)
                    Brushes.Add(new SolidBrush(c));
                while (Y < Bottom)
                {
                    while (X < Right)
                    {
                        graphics.FillRectangle(Brushes[(CountX + CountY) % Length],
                            new Rectangle(X, Y, BlockWidth, BlockHeight));
                        X += BlockWidth;
                        CountX++;
                    }
                    X = StartX;
                    Y += BlockHeight;
                    CountY++;
                    CountX = 0;
                }
            }
        }
    }
}
