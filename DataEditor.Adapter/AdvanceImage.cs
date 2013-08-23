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
        ImageArguments argument;
        public AdvanceImage (XmlNode Node)
        {
            argument = new ImageArguments(Node);
        }

        public static System.Drawing.Rectangle Split (System.Drawing.Rectangle rect, ImageSplitType type, int x_value, int y_value, int index = -1, int x_index = 0, int y_index = 0)
        {
            // 首先，拆分图像并得到数据。
            int x_width = rect.Width, y_height = rect.Height, x_count, y_count;
            if ( type == ImageSplitType.Piece )
            {
                x_count = x_value;
                y_count = y_value;
                x_width /= x_count;
                y_height /= y_count;
            }
            else
            {
                x_count = x_width / x_value;
                y_count = y_height / y_value;
                x_width = x_value;
                y_height = y_value;
            }
            // 然后，计算图像的位置。
            if ( index > 0 ) { x_index = index % x_count; y_index = index / x_count; }
            return new System.Drawing.Rectangle(rect.X + x_index * x_width, rect.Y + y_index * y_height, x_width, y_height);
        }

        public enum ImageSplitType { Piece, Size };
        public abstract class NodeDescriper
        {
            public NodeDescriper () { }
            public NodeDescriper (XmlNode Node) { Load(Node); }
            protected virtual void Load (XmlNode Node)
            {
                foreach ( XmlNode child in Node.ChildNodes )
                    OnScan(child.Name.ToUpper(), child.InnerText);
                foreach ( XmlAttribute attr in Node.Attributes )
                    OnScan(attr.Name.ToUpper(), attr.InnerText);
                OnScan("INNERTEXT", Node.InnerText);
            }
            protected abstract void OnScan (string Name, string InnerText);
            public static int GetInt (string InnerText)
            {
                int x = -1;
                int.TryParse(InnerText, out x);
                return x;
            }
        }
        public class ImageSplit : NodeDescriper, Contract.Iconic
        {
            public ImageSplitType Type { get; set; }
            public int XValue { get; set; }
            public int YValue { get; set; }
            public string Flag { get { return flag; } }
            protected string flag;
            public ImageSplit (ImageSplitType type, int x, int y) { Type = type; XValue = x; YValue = y; }
            public ImageSplit (XmlNode Node)
            {
                Type = ImageSplitType.Piece;
                XValue = 4;
                YValue = 2;
                Load(Node);
            }
            protected override void OnScan (string Name, string InnerText)
            {
                if ( Name == "FLAG" ) flag = InnerText;
                else if ( Name == "TYPE" ) Type = (InnerText == "SIZE" ? ImageSplitType.Size : ImageSplitType.Piece);
                else if ( Name == "WIDTH" || Name == "X" ) XValue = GetInt(InnerText);
                else if ( Name == "HEIGHT" || Name == "Y" ) YValue = GetInt(InnerText);
                if ( XValue < 0 ) XValue = 4;
                if ( YValue < 0 ) YValue = 2;
            }
        }
        public class ImageIndex : NodeDescriper, Contract.Iconic
        {
            public int XValue { get; set; }
            public int YValue { get; set; }
            public string Flag { get { return flag; } }
            string flag;
            public ImageIndex (XmlNode Node) : base() { Load(Node); }
            protected override void OnScan (string Name, string InnerText)
            {
                if ( Name == "FLAG" ) flag = InnerText;
                else if ( Name == "X" ) XValue = GetInt(InnerText);
                else if ( Name == "Y" ) YValue = GetInt(InnerText);
            }
        }
        public class ImageSplitIndex : NodeDescriper
        {
            Dictionary<string, ImageIndex> indecies = new Dictionary<string, ImageIndex>();
            ImageIndex DefaultIndex;
            public int XValue { get; set; }
            public int YValue { get; set; }
            public ImageSplitType Type { get; set; }
            public ImageSplitIndex (XmlNode Node) { XValue = 1; YValue = 1; Load(Node); }
            protected override void Load (XmlNode Node)
            {
                foreach ( XmlAttribute attr in Node.Attributes )
                    OnScan(attr.Name.ToUpper(), attr.InnerText);
                foreach ( XmlNode child in Node.ChildNodes )
                    if ( child.Name.ToUpper() == "INDEX" )
                    {
                        ImageIndex ix = new ImageIndex(child);
                        indecies.Add(ix.Flag, ix);
                        if ( ix.Flag == "" ) DefaultIndex = ix;
                    }
                    else OnScan(child.Name, child.InnerText);
            }
            protected override void OnScan (string Name, string InnerText)
            {
                if ( Name == "TYPE" ) Type = (InnerText == "SIZE" ? ImageSplitType.Size : ImageSplitType.Piece);
                else if ( Name == "WIDTH" || Name == "X" ) XValue = GetInt(InnerText);
                else if ( Name == "HEIGHT" || Name == "Y" ) YValue = GetInt(InnerText);
            }
            public ImageIndex GetIndex (string Name)
            {
                ImageIndex ix = DefaultIndex;
                string Key = Name[0].ToString();
                indecies.TryGetValue(Key, out ix);
                return ix;
            }

        }
        public class ImageArguments
        {
            Dictionary<string, ImageSplit> Splits = new Dictionary<string, ImageSplit>();
            ImageSplit DefaultSplit = null;
            ImageSplitIndex SplitIndex = null;

            public ImageArguments (XmlNode Node) { Load(Node); }
            protected void Load (XmlNode Node)
            {
                foreach ( XmlNode child in Node.ChildNodes )
                    if ( child.Name.ToUpper() == "SPLIT" )
                        ScanSplit(Node);
                    else if ( child.Name.ToUpper() == "INDEX" )
                        SplitIndex = new ImageSplitIndex(child);
            }
            protected void ScanSplit (XmlNode SplitNode)
            {
                foreach ( XmlNode split in SplitNode.ChildNodes )
                {
                    ImageSplit sp = new ImageSplit(split);
                    Splits.Add(sp.Flag, sp);
                    if ( sp.Flag == "" ) DefaultSplit = sp;
                }
            }
            public ImageSplit GetSplit (ref string Name)
            {
                ImageSplit sp = DefaultSplit;
                string key = Name[0].ToString();
                Splits.TryGetValue(key, out sp);
                if ( sp.Flag != "" ) Name = Name.Remove(0, sp.Flag.Length);
                return sp;
            }
            public ImageSplitIndex GetIndex () { return SplitIndex; }
        }
    }
}
