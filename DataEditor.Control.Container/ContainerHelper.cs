using System;
using System.Collections.Generic;
using System.Text;
using DataEditor.FuzzyData;

namespace DataEditor.Control.Container
{
    public class ContainerHelper
    {
        private FuzzySymbol childSymbol;
        private FuzzyObject topValue;
        private FuzzyObject parentValue;
        public event EventHandler<ValueChangedEventArgs> ChildValueChanged;
        /// <summary>
        /// 获取或设置此容器的值
        /// </summary>
        public FuzzyObject ParentValue 
        { 
            get { return parentValue; }
            set { parentValue = value; OnChildValueChanged(); }
        }
        /// <summary>
        /// 获取此容器的子值。
        /// 若设置了此容器的子值，则它可以没有父值。
        /// </summary>
        public FuzzyObject ChildValue 
        {
            get
            {
                if (topValue != null) return topValue;
                if (ChildSymbol == null) return parentValue;
                if (parentValue.InstanceVariables.ContainsKey(ChildSymbol))
                    return parentValue.InstanceVariables[childSymbol] as FuzzyObject ?? parentValue;
                else return parentValue;
            }
            set { topValue = value; OnChildValueChanged(); }
        }
        protected virtual void OnChildValueChanged()
        {
            if (ChildValueChanged != null)
                ChildValueChanged(this, new ValueChangedEventArgs(this));
        }
        /// <summary>
        /// 当此值为 null 时，ChildValue 即为 ParentValue 的值
        /// 当此值非空时，ChildValue 为 ParentValue 的这一子项
        /// </summary>
        public FuzzySymbol ChildSymbol
        {
            get { return childSymbol; }
            set { childSymbol = value; OnChildValueChanged(); }
        }

        public class ValueChangedEventArgs : EventArgs 
        {
            FuzzyObject newValue;
            public ValueChangedEventArgs(FuzzyObject Value)
            {
                newValue = Value;
            }
            public ValueChangedEventArgs(ContainerHelper s) : this(s.ChildValue) { }
        }
    }
}
