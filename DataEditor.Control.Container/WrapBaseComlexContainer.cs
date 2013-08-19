using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Container
{
    public abstract class WrapBaseComplexContainer<TValue, TArg> : WrapBaseContainer<TArg>
        where TValue : FuzzyData.FuzzyObject
        where TArg : ComplexContainerArgs, new()
    {
        // 契约：必须重写 Push
        // 契约：必须重写 Pull
        protected new TValue value;
        protected new virtual TValue ConvertToValue (FuzzyData.FuzzyObject origin) { return origin as TValue; }
        public override FuzzyData.FuzzyObject Parent
        {
            set
            {
                FuzzyData.FuzzyObject origin = ControlHelper.TypeCheck<FuzzyData.FuzzyObject>.Get(value, key);
                TValue ans = ConvertToValue(origin);
                if ( ans != null ) this.value = ans;
                Pull();
                base.Parent = value;
            }
        }
        public new virtual TValue Value
        {
            get { return value; }
            set { this.value = ConvertToValue(value); Pull(); }
        }
        public FuzzyData.FuzzyObject ContainerValue
        {
            get { return base.Value; }
            set { base.Value = value; }
        }
    }
}