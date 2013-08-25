using System;
using System.Collections.Generic;
using System.Text;
using DataEditor.Adapter;

namespace DataEditor.Control.Wrapper
{
    public class WrapImage //: WrapBaseEditor<AdvanceImage,ImageArgs>
    {
    //    Wrapper.WrapImage actual;
    //    public override void Push ()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override void Pull ()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override void Bind ()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override string Flag
    //    {
    //        get { return "image"; }
    //    }
    }
    public class ImageArgs : ControlArgs
    {
        public FuzzyData.FuzzySymbol Index { get; set; }
        public FuzzyData.FuzzySymbol Hue { get; set; }
        public string Path { get; set; }
        public SplitSearcher Split { get; set; }
        public SplitSearcher ReSplit { get; set; }
        protected List<AdvanceImage.AdvanceImageRect> Splits = new List<AdvanceImage.AdvanceImageRect>();
        protected List<AdvanceImage.AdvanceImageRect> Indecis = new List<AdvanceImage.AdvanceImageRect>();

        public ImageArgs () 
        {
            Index = null;
            Hue = null;
            Path = "";
            Split = new SplitSearcher(Splits);
            ReSplit = new SplitSearcher(Indecis);
        }
        public ImageArgs (System.Xml.XmlNode Node) : this() { Load(Node); }

        public override void Load (System.Xml.XmlNode Node)
        {
            Splits.Clear();
            Indecis.Clear();
            string Name;
            foreach ( System.Xml.XmlNode child in Node.ChildNodes )
            {
                Name = child.Name.ToUpper();
                if ( Name == "SPLIT" )
                    foreach ( System.Xml.XmlNode split in child.ChildNodes )
                    {
                        ImageSplitArgs sa = new ImageSplitArgs(split);
                        Splits.Add(new AdvanceImage.AdvanceImageRect(sa.Type, sa.XValue, sa.YValue, 0, 0, sa.Flag));            
                    }
                else if ( Name == "INDEX" )
                {
                    ImageIndexGeneralArgs iiga = new ImageIndexGeneralArgs(child);
                    foreach ( System.Xml.XmlNode index in child.ChildNodes )
                    {
                        ImageIndexArgs ia = new ImageIndexArgs(index);
                        Indecis.Add(new AdvanceImage.AdvanceImageRect(iiga.Type, iiga.X_ALL, iiga.Y_ALL, ia.X, ia.Y, ia.Flag));
                    }
                }
                else OnScan(Name, child.InnerText);
            }
            foreach ( System.Xml.XmlAttribute attr in Node.Attributes )
                OnScan(attr.Name.ToUpper(), attr.Value);
        }
        protected override void OnScan (string Name, string InnerText)
        {
            if ( Name == "INDEX" ) Index = GetSymbol(InnerText);
            else if ( Name == "HUE" ) Hue = GetSymbol(InnerText);
            else if ( Name == "PATH" ) Path = InnerText;
            base.OnScan(Name, InnerText);
        }

        class ImageBaseArgs : ControlArgs
        {
            protected AdvanceImage.ImageSplitType GetImageType (string Value)
            { return Value.ToUpper() == "SIZE" ? AdvanceImage.ImageSplitType.Size : AdvanceImage.ImageSplitType.Piece; }
        }
        class ImageSplitArgs : ImageBaseArgs, Contract.Iconic
        {
            public int XValue { get; set; }
            public int YValue { get; set; }
            public AdvanceImage.ImageSplitType Type { get; set; }
            public string Flag { get; set; }
            public ImageSplitArgs () { XValue = YValue = 0; Type = AdvanceImage.ImageSplitType.Piece; Flag = ""; }
            public ImageSplitArgs (System.Xml.XmlNode Node) : this() { Load(Node); }
            protected override void OnScan (string Name, string InnerText)
            {
                if ( Name == "X" ) XValue = GetInt(InnerText);
                else if ( Name == "Y" ) YValue = GetInt(InnerText);
                else if ( Name == "FLAG" ) Flag = InnerText;
                else if ( Name == "TYPE" ) Type = GetImageType(InnerText);
            }
        }
        class ImageIndexArgs : ImageSplitArgs 
        {
            public ImageIndexArgs () : base() { }
            public ImageIndexArgs (System.Xml.XmlNode Node) : this() { Load(Node); }
            public int X { get { return XValue; } }
            public int Y { get { return YValue; } }
        }
        class ImageIndexGeneralArgs : ImageSplitArgs
        {
            public ImageIndexGeneralArgs () : base() { }
            public ImageIndexGeneralArgs (System.Xml.XmlNode Node) : this() { Load(Node); }
            public int X_ALL { get { return XValue; } }
            public int Y_ALL { get { return YValue; } }
        }
        public class SplitSearcher
        {
            List<AdvanceImage.AdvanceImageRect> actual;
            public SplitSearcher (List<AdvanceImage.AdvanceImageRect> para) { actual = para; }
            public AdvanceImage.AdvanceImageRect this[string name]
            {
                get 
                {
                    if(actual.Count == 0) return null;
                    foreach ( var r in actual )
                        if ( name.StartsWith(r.Flag) )
                            return r;
                    return actual[0];
                }
            }
        }
    }
} 
