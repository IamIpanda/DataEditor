using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control
{
    public abstract class WrapBaseEditor<TValue, TArg> : ObjectEditor
        where TValue : FuzzyData.FuzzyObject
        where TArg : ControlArgs, new()
    {
        protected TArg argument;
        protected TValue value;
        protected FuzzyData.FuzzySymbol key;

        public abstract void Push ();
        public abstract void Pull ();
        public abstract void Bind ();
        public abstract string Flag { get; }

        public virtual ObjectEditor Container { get; set; }
        public virtual System.Windows.Forms.Label Label { get; set; }
        public virtual System.Windows.Forms.Control Binding { get; set; }

        public WrapBaseEditor () { Bind(); }
        public virtual void OnEnter (object sender, EventArgs e) { }
        public virtual void OnLeave (object sender, EventArgs e) { if ( CheckValue() )OnValueChanged(); Push(); }

        protected virtual TValue ConvertToValue (FuzzyData.FuzzyObject origin) { return origin as TValue; }
      
        public virtual ControlArgs Load_Information (System.Xml.XmlNode Node)
        {
            TArg argument = new TArg();
            argument.Load(Node);
            return argument;
        }
        public virtual ControlArgs Arguments
        {
            get { return argument; }
            set { argument = value as TArg; Reset(); }
        }
        public virtual FuzzyData.FuzzyObject Value
        {
            get { return value; }
            set
            {
                TValue ans = ConvertToValue(value);
                if ( ans == null ) return;
                this.value = ans;
                PullTaint();
                Pull();
            }
        }
        public virtual FuzzyData.FuzzyObject Parent
        {
            set
            {
                FuzzyData.FuzzyObject origin = ControlHelper.TypeCheck<FuzzyData.FuzzyObject>.Get(value, key);
                TValue ans = ConvertToValue(origin);
                if ( ans == null ) return;
                this.value = ans;
                PullTaint();
                Pull();
            }
        }
        public virtual void Reset ()
        {
            if ( argument == null ) return;
            if ( Binding == null ) return;
            DataEditor.Control.ControlHelper.Reset(this, argument);
            this.key = argument.Actual;
            Binding.Enter += OnEnter;
            Binding.Leave += OnLeave;
        }

        protected virtual bool CheckValue () { return false; }
        protected virtual void PullTaint () { ShowTaint(Help.TaintRecord.Single[Value]); }
        protected virtual void PushTaint () { if ( Value != null )Help.TaintRecord.Single[Value] = Contract.TaintState.Tainted; }
        protected virtual void ShowTaint (Contract.TaintState State) { if ( Label != null ) Label.ForeColor = Help.TaintOptions.DefaultColors[State]; }
        protected virtual void OnValueChanged ()
        {
            Help.Log.log(this.GetType().ToString() + " " + (Label == null ? "" : Label.Text) + "发出了一个 Taint ");
            PushTaint();
            ShowTaint(Contract.TaintState.Tainted);
            TaintableContainer container = Container as TaintableContainer;
            if ( container != null ) container.OnChildTainted(this);
        }
    }
}
