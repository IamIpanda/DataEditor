using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using DataEditor.FuzzyData.Serialization;

namespace DataEditor.FuzzyData
{
    public class FuzzyTone : FuzzyColor
    {
        public FuzzyTone(int r, int g, int b, int gray) : base(r, g, b, gray) { }
        public FuzzyTone(System.Drawing.Color c) : base(c) { }
        public int Gray
        {
            get { return base.alpha; }
            set { base.alpha = value; }
        }
        protected new XmlNode ToDocument(XmlDocument document)
        {
            XmlNode Parent = document.CreateNode(XmlNodeType.Element, "Tone", "");
            XmlNode R = document.CreateNode(XmlNodeType.Element, "R", ""); R.InnerText = base.red.ToString();
            XmlNode G = document.CreateNode(XmlNodeType.Element, "G", ""); G.InnerText = base.green.ToString();
            XmlNode B = document.CreateNode(XmlNodeType.Element, "B", ""); B.InnerText = base.blue.ToString();
            XmlNode A = document.CreateNode(XmlNodeType.Element, "A", ""); A.InnerText = base.alpha.ToString();
            Parent.AppendChild(R);
            Parent.AppendChild(G);
            Parent.AppendChild(B);
            Parent.AppendChild(A);
            return Parent;
        }
        public class FuzzyToneFactoty : ISerializationFactory<byte[]>,
    ISerializationFactory<XmlNode>
        {
            public byte[] dump(Stream stream, object color, byte[] Tag)
            {
                (color as FuzzyTone).ToBytes(stream);
                return new byte[0];
            }
            public XmlNode dump(Stream stream, object color, XmlNode Node)
            {
                return (color as FuzzyTone).ToDocument(new XmlDocument());
            }
            public object _dump(Stream stream, byte[] bytes)
            {
                return FuzzyTone.GetBytes(stream);
            }
            public object _dump(Stream stream, XmlNode node)
            {
                return FuzzyTone.GetDocument(node);
            }
            public string Type
            {
                get { return "Tone"; }
            }
            public Type Actual
            {
                get { return typeof(FuzzyTone); }
            }
        }
    }

}
