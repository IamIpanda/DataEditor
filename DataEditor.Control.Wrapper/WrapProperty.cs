using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper
{
    public class WrapProperty : WrapBaseEditor<Adapter.AdvanceArray ,PropertyArgs>
    {
        Prototype.ProtoListView actual;
        System.Xml.XmlNode dialog;
        FuzzyData.FuzzyObject New;
        List<PropertyArgs.PropertyColumnArgs> columnArgs;
        List<FuzzyData.FuzzySymbol> parameters;
        public override string Flag { get { return "property"; } }
        public override void Push () { }
        public override void Pull () { }
        public override void Bind ()
        {
            Binding = actual = new Prototype.ProtoListView();
            actual.DoubleClick += actual_DoubleClick;
        }

        void actual_DoubleClick (object sender, EventArgs e)
        {
        }
        public override void Reset ()
        {
            base.Reset();
            if ( argument == null ) return;
            dialog = argument.Dialog;
            New = argument.New;
            columnArgs = argument.Columns;
            parameters = argument.Parameters;
            if ( actual == null ) return;
            foreach ( var row in argument.Columns )
            {
                int w = row.Width == -1 ? 200 : row.Width;
                actual.Columns.Add(row.Text, w);
            }
        }

        protected override Adapter.AdvanceArray ConvertToValue (FuzzyData.FuzzyObject origin)
        {
            FuzzyData.FuzzyArray arr = origin as FuzzyData.FuzzyArray;
            if ( arr == null ) return null;
            Adapter.AdvanceArray array = new Adapter.AdvanceArray(arr);
            return array.Exists ? array : null;
        }

        protected object[] GetParameters (FuzzyData.FuzzyObject parent = null)
        {
            if ( parent == null ) parent = SelectedValue;
            if ( parent == null ) return null;
            List<object> paras = new List<object>();
            foreach ( FuzzyData.FuzzySymbol sym in parameters )
                paras.Add(GetParameter(parent, sym));
            return paras.ToArray();
        }
        protected object GetParameter (FuzzyData.FuzzyObject Parent, FuzzyData.FuzzySymbol Sym)
        {
            if ( Parent == null || Sym == null ) return null;
            object value = null;
            Parent.InstanceVariables.TryGetValue(Sym, out value);
            return value;
        }
        public FuzzyData.FuzzyObject SelectedValue
        {
            get 
            {
                if ( actual == null ) return null;
                if ( actual.SelectedItems.Count == 0 ) return null;
                if ( value == null ) return FuzzyData.FuzzyNil.Instance;
                return value.Data[actual.SelectedIndices[0]];
            }
        }
    }
   public class PropertyArgs : ListViewArgs
   {
       public FuzzyData.FuzzySymbol Code { get; set; }
       public List<FuzzyData.FuzzySymbol> Parameters { get; set; }
       public new List<PropertyColumnArgs> Columns { get; set; }

       public PropertyArgs ()
       {
           Code = null;
           Parameters = new List<FuzzyData.FuzzySymbol>();
           Columns = new List<PropertyColumnArgs>();
       }
       public PropertyArgs (System.Xml.XmlNode Node) : this() { Load(Node); }
       public override void Load (System.Xml.XmlNode Node)
       {
           base.OriginLoad(Node);
       }

       protected override void OnScan (string Name, string InnerText)
       {
           if ( Name == "CODE" ) Code = GetSymbol(InnerText);
           else if ( Name == "PARAMETER" ) Parameters.Add(GetSymbol(InnerText));
           base.OnScan(Name, InnerText);
       }

       protected override void OnCheck (string Name, System.Xml.XmlNode Node)
       {
           if ( Name == "COLUMN" ) Columns.Add(new PropertyColumnArgs(Node));
           else if ( Name == "NEW" ) New = LoadXmlData(Node.FirstChild);
           else if ( Name == "DIALOG" ) Dialog = Node;
           base.OnCheck(Name, Node);
       }
       public class PropertyColumnArgs : PropertyArgs.ListViewColumnArgs
       {
           public  new  Dictionary<int, string> Format { get; set; }
           public bool AllString { get; set; }
           public PropertyColumnArgs () { Format = new Dictionary<int, string>(); AllString = false; }
           public PropertyColumnArgs (System.Xml.XmlNode Node) : this() { Load(Node); }
           protected override void OnScan (string Name, string InnerText)
           {
               if ( Name == "STRING" ) AllString = !AllString;
               base.OnScan(Name, InnerText);
           }
           protected override void OnCheck (string Name, System.Xml.XmlNode Node)
           {
               if ( Name == "TEXTS" )
               {
                   foreach ( System.Xml.XmlNode child in Node.ChildNodes )
                       if ( child.Name.ToUpper() == "TEXT" )
                       {
                           int index = -1;
                           foreach ( System.Xml.XmlAttribute attr in child.Attributes )
                               if ( attr.Name.ToUpper() == "CODE" )
                                   index = GetInt(attr.InnerText);
                           if ( index > 0 )
                               Format.Add(index, child.InnerText);
                       }
               }
               base.OnCheck(Name, Node);
           }
       }
   }
}
