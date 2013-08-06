using System;
using System.Collections.Generic;
using System.Text;
using DataEditor.Contract;

namespace DataEditor.Help
{
    public class TaintIndexCollection : TaintCollection
    {
        List<TaintState> taints;
        public TaintIndexCollection(int length)
        {
            taints = new List<TaintState>(length);
            for (int i = 0; i < length; i++)
                taints.Add(TaintState.UnTainted);
        }
        public TaintState this[int index]
        {
            get 
            {
                while (index >= taints.Count)
                    taints.Add(TaintState.UnTainted);
                if (index >= 0 && index < taints.Count)
                    return taints[index];
                return TaintState.UnTainted;
            }
            set
            {
                while (index >= taints.Count)
                    taints.Add(TaintState.UnTainted);
                if (index >= 0)
                {
                    TaintState former = taints[index];
                    taints[index] = value;
                    if (TaintIndexChanged != null)
                        TaintIndexChanged(this, new TaintIndexChangedEventArgs(index, former, value));
                }
            }
        }
        public int Count
        {
            get { return taints.Count; }
        }
        public event EventHandler<TaintIndexChangedEventArgs> TaintIndexChanged;
    }
    public class TaintIndexChangedEventArgs : EventArgs
    {
        public int Index { get; set; }
        public TaintState Former { get; set; }
        public TaintState Latter { get; set; }
        public TaintIndexChangedEventArgs(int index, TaintState former, TaintState latter)
        {
            this.Index = index;
            this.Former = former;
            this.Latter = latter;
        }
    }
}
