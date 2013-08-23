using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper
{
    public class WrapProperty : WrapBaseEditor<Adapter.AdvanceArray ,PropertyArgs>
    {
        Prototype.ProtoListView actual;
        public override void Push () { }
        public override void Pull () { }
        public override void Bind () { Binding = actual = new Prototype.ProtoListView(); }
        public override string Flag { get { return "property" } }
    }
   public class PropertyArgs : ListViewArgs
   {
       public String Code { get; set; }
       protected override void OnScan (string Name, string InnerText)
       {
           if ( Name == "CODE" ) Code = InnerText;
       }
   }
}
