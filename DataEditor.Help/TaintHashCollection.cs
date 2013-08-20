using System;
using System.Collections.Generic;
using System.Text;
using DataEditor.Contract;

namespace DataEditor.Help
{
    public class TaintHashCollection : TaintCollection
    {
        Dictionary<int, TaintState> taints = new Dictionary<int, TaintState>();
        public TaintState this[int index]
        {
            get
            {
                return TaintState.UnTainted;
            }
            set
            {
            }
        }
        public int Count { get { return taints.Count; } }
    }
}
