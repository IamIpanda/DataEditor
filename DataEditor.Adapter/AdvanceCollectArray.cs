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
            if ( collect == null ) collect = this.DefaultCollect;
            Exists = CollectArray(origin, collect);
        }
        protected virtual bool CollectArray (FuzzyData.FuzzyArray origin, AdvanceCollect collect)
        {
            foreach ( object ob in origin )
            {
                AdvanceCollectResult result = collect(ob);
                if ( result == AdvanceCollectResult.Denied ) return false;
                else if ( result == AdvanceCollectResult.Accepted ) SetAcceptedItem(ob);
                else if ( result == AdvanceCollectResult.Wrong ) SetWrongItem(ob);
            }
            return true;
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
        public List<T> Data { get { return data; } }
        public Help.LinkTable<int, int> Link { get { return link; } }

        protected List<T> data = new List<T>();
        protected Help.LinkTable<int, int> link = new Help.LinkTable<int, int>(); // i <= j

        private int i, j;

        public AdvanceCollectTypeArray (FuzzyData.FuzzyArray origin, AdvanceCollect collect = null)
            : base(origin, collect) { }
        protected override bool CollectArray (FuzzyData.FuzzyArray origin, AdvanceCollectArray.AdvanceCollect collect)
        {
            i = j = 0;
            data.Clear();
            link.Clear();
            bool b = base.CollectArray(origin, collect);
            // 核心超频连线！
            FuzzyData.FuzzyArray temp = this | origin;
            return b;
        }
        protected override void SetAcceptedItem (object item)
        {
            link.Add(i, j);
            data.Add(item as T);
            i += 1; j += 1;
        }
        protected override void SetWrongItem (object item)
        {
            i += 1;
        }
        protected override AdvanceCollectResult DefaultCollect (object item)
        {
            if ( item is FuzzyData.FuzzyNil ) return AdvanceCollectResult.Wrong;
            T obj = item as T;
            if ( obj == null ) return AdvanceCollectResult.Denied;
            return AdvanceCollectResult.Accepted;
        }

        // TODO : Add Function add / delete and so on
    }

    public class AdvanceCollectNameArray : AdvanceCollectTypeArray<FuzzyData.FuzzyObject>
    {
        public FuzzyData.FuzzySymbol ArrayClassName { get; set; }
        public AdvanceCollectNameArray (FuzzyData.FuzzyArray origin, AdvanceCollect collect = null)
            : base(origin, collect)
        {
            ClassName = null;
        }

        protected override AdvanceCollectResult DefaultCollect (object item)
        {
            if ( item is FuzzyData.FuzzyNil ) return AdvanceCollectResult.Wrong;
            FuzzyData.FuzzyObject  obj = item as FuzzyData.FuzzyObject;
            if ( obj == null ) return AdvanceCollectResult.Denied;
            if ( ArrayClassName == null ) ArrayClassName = obj.ClassName;
            else if ( ArrayClassName != obj.ClassName)
                return AdvanceCollectResult.Denied;
            return AdvanceCollectResult.Accepted;
        }
    }
}
