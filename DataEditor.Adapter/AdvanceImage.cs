using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

// TODO : Rebuild It.
namespace DataEditor.Adapter
{
    public class AdvanceImage
    {
        public FuzzyData.FuzzyString ImageName { get; set; }
        public FuzzyData.FuzzyFixnum ImageIndex { get; set; }
        public FuzzyData.FuzzyFixnum ImageTune { get; set; }

        public enum ImageSplitType { Piece, Size };
        public class AdvanceImageRect 
        {
            public ImageSplitType Type { get; set; }
            public int XValue { get; set; }
            public int YValue { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            public string Flag { get; set; }
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
                int x_width = rect.Width, y_height = rect.Height, x_count, y_count;
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
                if ( index > 0 ) { X = index % x_count; Y = index / x_count; }
                return new System.Drawing.Rectangle(rect.X + X * x_width, rect.Y + Y * y_height, x_width, y_height);
            }
        }
    }
}
