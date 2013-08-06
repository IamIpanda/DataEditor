using System;
using System.Collections.Generic;
using System.Text;
using DataEditor.FuzzyData;

namespace DataEditor.Adapter
{
    public class AdvanceCommand
    {
        protected FuzzyArray value;
        public FuzzyArray Value { get { return value; } }
        protected AdvanceCommand(FuzzyArray value)
        {
            this.value = value;
        }
        public int Mode { get { return (int)(value[0] as FuzzyFixnum).Value; } }
        static AdvanceCommand Get(FuzzyArray Value)
        {
            if (Value[0] is FuzzyFixnum || Value[0] is int)
                return new AdvanceCommand(Value);
            return null;
        }
    }
}
