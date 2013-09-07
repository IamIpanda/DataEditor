using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace DataEditor.Control
{
    public abstract class ControlArgs
    {
        public ControlArgs()
        {
            this.Width = -1;
            this.Height = -1;
            this.Label = 1;
            this.Text = "";
            this.Actual = null;
        }
        public ControlArgs(System.Xml.XmlNode node) : this()
        {
            Load(node);
        }
        public virtual void Load(System.Xml.XmlNode Node)
        {
            foreach (XmlAttribute attr in Node.Attributes)
                OnScan(attr.Name.ToUpper(), attr.InnerText);
            foreach (XmlNode node in Node.ChildNodes)
                if ( node.NodeType == XmlNodeType.Text )
                    OnScan("INNERTEXT", node.Value);
                else
                {
                    OnScan(node.Name.ToUpper(), GetValue(node));
                    OnCheck(node.Name.ToUpper(), node);
                }
        }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Label { get; set; }
        public string Text { get; set; }
        public FuzzyData.FuzzySymbol Actual { get; set; }

        protected virtual void OnScan (string Name, string InnerText)
        {
            if ( Name == "WIDTH" )
                this.Width = GetInt(InnerText);
            else if ( Name == "HEIGHT" )
                this.Height = GetInt(InnerText);
            else if ( Name == "LABEL" )
                this.Label = GetInt(InnerText);
            else if ( Name == "TEXT" )
                this.Text = InnerText;
            else if ( Name == "ACTUAL" )
                this.Actual = GetSymbol(InnerText);
        }
        protected virtual void OnCheck (String Name, XmlNode Node) { }
        protected int GetInt(string str)
        {
            int i;
            if (int.TryParse(str, out i))
                return i;
            return -1;
        }
        protected double GetDouble (string str)
        {
            double d;
            return double.TryParse(str, out d) ? d : -1;
        }
        protected System.Drawing.Color GetColor(string s)
        {
            int i = GetInt(s);
            if (i == -1) return default(System.Drawing.Color);
            System.Drawing.Color c = System.Drawing.Color.FromArgb(i);
            if (c.A == 0)
                c = System.Drawing.Color.FromArgb(255, c);
            return c;
        }
        protected string GetValue(XmlNode node)
        {
            foreach (XmlNode child in node.ChildNodes)
                if (child.NodeType == XmlNodeType.Text)
                    return child.Value;
            return "";
        }
        protected List<XmlNode> GetChild(XmlNode node)
        {
            List<XmlNode> children = new List<XmlNode>();
            foreach (XmlAttribute attr in node.Attributes)
                children.Add(attr);
            foreach (XmlNode child in node.ChildNodes)
                children.Add(child);
            return children;
        }
        protected FuzzyData.FuzzySymbol GetSymbol (string s)
        {
            if ( !s.StartsWith("@") ) s = "@" + s;
            return FuzzyData.FuzzySymbol.GetSymbol(s);
        }
    }
}
