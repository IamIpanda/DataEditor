using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper
{
    public class WrapProperty : WrapBaseEditor<Adapter.AdvanceArray ,PropertyArgs>
    {
        Prototype.ProtoListView actual;
        public override void Push () { }
        public override void Pull () { 
        }
        public override void Bind () 
        {
            Binding = actual = new Prototype.ProtoListView(); 
        }
        public override string Flag { get { return "property"; } }
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
               if ( Name == "TEXT" )
               {
                   int index = -1;
                   foreach ( System.Xml.XmlAttribute attr in Node.Attributes )
                       if ( attr.Name.ToUpper() == "CODE" )
                           index = GetInt(attr.InnerText);
                   if ( index > 0 )
                       Format.Add(index, Node.InnerText);
               }
               base.OnCheck(Name, Node);
           }
       }
   }
}
