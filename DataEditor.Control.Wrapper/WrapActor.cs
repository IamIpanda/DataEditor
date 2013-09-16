using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper
{
    public class WrapActor : WrapBaseEditor<FuzzyData.FuzzyTable, ActorArgs>
    {
        public override void Push () { }
        public override void Pull () { }

        public override void Bind ()
        {
        }

        public override string Flag { get { return "actor"; } }
    }
    public class ActorArgs : ControlArgs 
    {

    }
}
