using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DataEditor.Control.Prototype
{
    public class ProtoColoredListBox : ProtoListBox
    {
        ColorListCollection Colors = new ColorListCollection();
        public ProtoColoredListBox()
        {
            base.RightShift = 12;
        }
        public ColorListCollection Color { get { return Colors; } }
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);
            if (e.Index < 0 || e.Index >= Items.Count)
                return;
            if (e.Index >= Colors.ActualValue.Count)
                return;
            Brush ColorBrush = new SolidBrush(Colors[e.Index]);
            e.Graphics.FillEllipse(ColorBrush, e.Bounds.X + 1, e.Bounds.Y + 1, 10, 10);
        }
    }
    public class ColorListCollection : Help.SimpleCollection<Color>
    {
        public ColorListCollection() : base(new List<Color>()) { }
        public ColorListCollection(List<Color> list) : base(list) { }
    }
}