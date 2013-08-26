using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

// TODO : Rebuild It.
namespace DataEditor.Adapter
{
    public class AdvanceImage : FuzzyData.FuzzyObject
    {
        public FuzzyData.FuzzyString ImageName { get; set; }
        public FuzzyData.FuzzyFixnum ImageIndex { get; set; }
        public FuzzyData.FuzzyFixnum ImageHue { get; set; }
        public AdvanceImage (FuzzyData.FuzzyString image_name, FuzzyData.FuzzyFixnum image_index, FuzzyData.FuzzyFixnum image_hue)
        {
            ImageName = image_name;
            ImageIndex = image_index;
            ImageHue = image_hue;
        }
        public override void Clone (FuzzyData.FuzzyObject source)
        {
            AdvanceImage image = source as AdvanceImage;
            if ( image == null ) return;
            FuzzyData.FuzzyObject t;
            t = this.ImageName & image.ImageName;
            t = this.ImageIndex & image.ImageIndex;
            t = this.ImageHue & image.ImageHue;
            base.Clone(source);
        }
        public override bool Equals (object obj)
        {
            AdvanceImage ai = obj as AdvanceImage;
            if ( ai == null ) return false;
            return ImageName == ai.ImageName && ImageIndex == ai.ImageIndex && ImageHue == ai.ImageHue;
        }
        public override object Clone ()
        {
            FuzzyData.FuzzyString n = ImageName == null ? null : ImageName.Clone() as FuzzyData.FuzzyString;
            FuzzyData.FuzzyFixnum i = ImageIndex == null ? null : ImageIndex.Clone() as FuzzyData.FuzzyFixnum;
            FuzzyData.FuzzyFixnum h = ImageHue == null ? null : ImageHue.Clone() as FuzzyData.FuzzyFixnum;
            return new AdvanceImage(n, i, h);
        }

        public override int GetHashCode ()
        {
            int i = 0;
            if ( ImageName != null ) i += ImageName.GetHashCode();
            if ( ImageIndex != null ) i += ImageIndex.GetHashCode();
            if ( ImageHue != null ) i += ImageHue.GetHashCode();
            return i;
        }

        public enum ImageSplitType { Piece, Size };
        public class AdvanceImageRect 
        {
            public ImageSplitType Type { get; set; }
            public int XValue { get; set; }
            public int YValue { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            public string Flag { get; set; }
            protected int x_count = 0, y_count = 0, x_width = 0, y_height = 0;
            public AdvanceImageRect (ImageSplitType type, int x_value, int y_value, int x, int y,string flag)
            {
                Type = type;
                XValue = x_value;
                YValue = y_value;
                X = x;
                Y = y;
                Flag = flag;
            }
            public System.Drawing.Rectangle Split (System.Drawing.Rectangle rect, int index = -1)
            {
                // 首先，拆分图像并得到数据。
                x_width = rect.Width;
                y_height = rect.Height;
                if ( Type == ImageSplitType.Piece )
                {
                    x_count = XValue;
                    y_count = YValue;
                    x_width /= x_count;
                    y_height /= y_count;
                }
                else
                {
                    x_count = x_width / XValue;
                    y_count = y_height / YValue;
                    x_width = XValue;
                    y_height = YValue;
                }
                // 然后，计算图像的位置。
                if ( index > 0 ) SetIndex(index);
                return new System.Drawing.Rectangle(rect.X + X * x_width, rect.Y + Y * y_height, x_width, y_height);
            }
            public int this[int x, int y] { get { return y * x_count + x; } }
            public int Blocks { get { return x_count * y_count; } }
            public void SetIndex (int index)
            {
                if ( x_count <= 0 ) return;
                X = index % x_count;
                Y = index / x_count;
            }
            public void SetIndex (int mouse_x, int mouse_y)
            {
                X = mouse_x / x_width; if ( X >= x_count ) X = x_count - 1;
                Y = mouse_y / y_height; if ( Y >= y_count ) Y = y_count - 1;
                if ( X < 0 ) X = 0; if ( Y < 0 ) Y = 0;
            }
        }
    }
}
