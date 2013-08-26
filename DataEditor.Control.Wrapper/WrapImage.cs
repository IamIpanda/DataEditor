using System;
using System.Collections.Generic;
using System.Text;
using DataEditor.Adapter;

namespace DataEditor.Control.Wrapper
{
    public class WrapImage : WrapBaseEditor<AdvanceImage,ImageArgs>
    {
         Prototype.ProtoImageBackgroundDisplayer actual;
        public override string Flag { get { return "image"; } }
        public override void Bind ()
        {
            Binding = actual = new Prototype.ProtoImageBackgroundDisplayer();
            actual.Scale = false;
            actual.UseRectangleFocus = true;
            actual.ImageAlignCenter = true;
            actual.FullBackgroundDraw = true;
            actual.DoubleClick += actual_DoubleClick;
        }

        void actual_DoubleClick (object sender, EventArgs e)
        {
            if ( actual == null || value == null || argument == null ) return;
            if ( value.ImageName != null )
            {
                Window.ImageChoser choser = new Window.ImageChoser();
                choser.Text = "更改图像";
                choser.Value = value;
                choser.Path = ImagePath;
                choser.Splits = argument.SplitList;
                choser.ReSplits = argument.ReSplitList;
                if ( choser.ShowDialog() == System.Windows.Forms.DialogResult.OK )
                { Pull(); OnValueChanged(); }
            }
            else 
            {
                Window.SingleImageChoser choser = new Window.SingleImageChoser();
                choser.Text = "更改图像";
                choser.Value = value;
                choser.Path = ImagePath;
                choser.Splits = argument.SplitList;
                choser.ReSplits = argument.ReSplitList;
                if ( choser.ShowDialog() == System.Windows.Forms.DialogResult.OK )
                { Pull(); OnValueChanged(); }
            }
        }
        protected FuzzyData.FuzzySymbol key_index = null, key_hue = null;
        protected string ImagePath;
        public override void Push ()
        {
        }

        public override void Pull ()
        {
            if ( value == null || actual == null ||  argument == null ) return;
            // 拼接字串，得到路径
            string AllPath;
            if ( value.ImageName != null ) AllPath = System.IO.Path.Combine(ImagePath, value.ImageName.Text);
            else AllPath = ImagePath;
            // 根据路径检索文件
            string s = Help.RtpManager.SearchFile(AllPath);
            // 得到位图文件
            var bitmap = new System.Drawing.Bitmap(s);
            // 根据文件名，检索路径矩形
            string name = System.IO.Path.GetFileName(s);
            // Fuuuuuuuu.... 为啥是Resplit的标识符在前！
            Adapter.AdvanceImage.AdvanceImageRect resplit = argument.ReSplit[name];
            if ( resplit != null ) name = name.Remove(0, resplit.Flag.Length);
            Adapter.AdvanceImage.AdvanceImageRect split = argument.Split[name];
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height);
            if ( split != null ) rect = split.Split(rect, (int)value.ImageIndex.Value);
            if ( resplit != null ) rect = resplit.Split(rect);
            // 上传图形文件
            actual.SrcRect = rect;
            actual.Bitmap = bitmap;
        }
        public override FuzzyData.FuzzyObject Parent
        {
            set
            {
                if ( value == null ) return;
                FuzzyData.FuzzyString Name = null;
                FuzzyData.FuzzyFixnum Index = null, Hue = null;
                if ( key != null ) Name = TypeCheck<FuzzyData.FuzzyString>.Check(value, key);
                if ( key_index != null ) Index = TypeCheck<FuzzyData.FuzzyFixnum>.Check(value, key_index);
                if ( key_hue != null ) Hue = TypeCheck<FuzzyData.FuzzyFixnum>.Check(value, key_hue);
                this.value = new AdvanceImage(Name, Index, Hue);
                PullTaint();
                Pull();
            }
        }
        public override void Reset ()
        {
            this.key_hue = argument.Hue;
            this.key_index = argument.Index;
            this.ImagePath = argument.Path;
            base.Reset();
        }
        protected override void ShowTaint (Contract.TaintState State)
        {
            if ( actual == null ) return;
            if ( State == Contract.TaintState.UnTainted )
            {
                actual.BackColors.Clear();
                actual.BackColors.Add(System.Drawing.Color.FromArgb(0,0,128));
                actual.BackColors.Add(System.Drawing.Color.FromArgb(0, 0, 65));
                actual.Invalidate();
            }
            else if ( State == Contract.TaintState.Tainted )
            {
                actual.BackColors.Clear();
                actual.BackColors.Add(System.Drawing.Color.FromArgb(128, 0, 0));
                actual.BackColors.Add(System.Drawing.Color.FromArgb(65, 0, 0));
                actual.Invalidate();
            }
            else if ( State == Contract.TaintState.Saved )
            {
                actual.BackColors.Clear();
                actual.BackColors.Add(System.Drawing.Color.FromArgb(0, 128, 0));
                actual.BackColors.Add(System.Drawing.Color.FromArgb(0, 65, 0));
                actual.Invalidate();
            }
            base.ShowTaint(State);
        }
        protected override bool CheckValue ()
        {
            return false;
        }
        class TypeCheck<T> where T : FuzzyData.FuzzyObject
        {
            static public T Check (FuzzyData.FuzzyObject value, FuzzyData.FuzzySymbol key)
            {
                object temp  = null;
                value.InstanceVariables.TryGetValue(key, out temp);
                if ( temp != null ) return temp as T;
                return null;
            }
        }
    }
    public class ImageArgs : ControlArgs
    {
        public FuzzyData.FuzzySymbol Index { get; set; }
        public FuzzyData.FuzzySymbol Hue { get; set; }
        public string Path { get; set; }
        public SplitSearcher Split { get; set; }
        public SplitSearcher ReSplit { get; set; }
        public List<AdvanceImage.AdvanceImageRect> SplitList { get { return Splits; } }
        public List<AdvanceImage.AdvanceImageRect> ReSplitList { get { return ReSplits; } }
        protected List<AdvanceImage.AdvanceImageRect> Splits = new List<AdvanceImage.AdvanceImageRect>();
        protected List<AdvanceImage.AdvanceImageRect> ReSplits = new List<AdvanceImage.AdvanceImageRect>();

        public ImageArgs () 
        {
            Index = null;
            Hue = null;
            Path = "";
            Split = new SplitSearcher(Splits);
            ReSplit = new SplitSearcher(ReSplits);
        }
        public ImageArgs (System.Xml.XmlNode Node) : this() { Load(Node); }

        public override void Load (System.Xml.XmlNode Node)
        {
            Splits.Clear();
            ReSplits.Clear();
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
                else if ( Name == "RESPLIT" )
                {
                    ImageResplitGeneralArgs iiga = new ImageResplitGeneralArgs(child);
                    foreach ( System.Xml.XmlNode index in child.ChildNodes )
                    {
                        ImageResplitArgs ia = new ImageResplitArgs(index);
                        ReSplits.Add(new AdvanceImage.AdvanceImageRect(iiga.Type, iiga.X_ALL, iiga.Y_ALL, ia.X, ia.Y, ia.Flag));
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
        class ImageResplitArgs : ImageSplitArgs 
        {
            public ImageResplitArgs () : base() { }
            public ImageResplitArgs (System.Xml.XmlNode Node) : this() { Load(Node); }
            public int X { get { return XValue; } }
            public int Y { get { return YValue; } }
        }
        class ImageResplitGeneralArgs : ImageSplitArgs
        {
            public ImageResplitGeneralArgs () : base() { }
            public ImageResplitGeneralArgs (System.Xml.XmlNode Node) : this() { Load(Node); }
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
                        if (r.Flag != "" && name.StartsWith(r.Flag) )
                            return r;
                    return actual[0];
                }
            }
        }
    }
} 
