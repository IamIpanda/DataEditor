using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Adapter
{
    public enum AdvanceCollectResult { Denied,Wrong,Accepted };
    public abstract class AdvanceCollectArray : FuzzyData.FuzzyArray
    {
        public bool Exists { get; set; }
        public delegate AdvanceCollectResult AdvanceCollect (object item);
        public AdvanceCollectArray (FuzzyData.FuzzyArray origin, AdvanceCollect collect = null)
        {
            Exists = false;
            if ( collect == null ) collect = this.DefaultCollect;
            foreach ( object ob in origin )
            {
                AdvanceCollectResult result = collect(ob);
                if ( result == AdvanceCollectResult.Denied ) return;
                else if ( result == AdvanceCollectResult.Accepted ) SetAcceptedItem(ob);
                else if ( result == AdvanceCollectResult.Wrong ) SetWrongItem(ob);
            }
        }
        protected virtual AdvanceCollectResult DefaultCollect (object item)
        {
            FuzzyData.FuzzyObject obj = item as FuzzyData.FuzzyObject;
            if ( obj == null ) return AdvanceCollectResult.Denied;
            if ( obj is FuzzyData.FuzzyNil ) return AdvanceCollectResult.Wrong;
            return AdvanceCollectResult.Accepted;
        }
        protected abstract void SetAcceptedItem (object item);
        protected virtual void SetWrongItem (object item) { }
    }
    // TODO ; Build it.
    public class AdvanceCollectTypeArray<T> : AdvanceCollectArray where T : FuzzyData.FuzzyObject
    {
        public List<T> Value { get { return data; } }
        public Help.LinkTable<int, int> Link { get { return link; } }

        protected List<T> data = new List<T>();
        protected Help.LinkTable<int, int> link = new Help.LinkTable<int, int>();

        private int i, j;

        protected override void SetAcceptedItem (object item)
        {
        }
        protected override void SetWrongItem (object item)
        {
        }
        protected override AdvanceCollectResult DefaultCollect (object item)
        {
            if ( item is FuzzyData.FuzzyNil ) return AdvanceCollectResult.Wrong;
            T obj = item as T;
            if ( obj == null ) return AdvanceCollectResult.Denied;
            return AdvanceCollectResult.Accepted;
        }
    }
}
