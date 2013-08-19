using System;
using System.Collections.Generic;
using System.Text;
using DataEditor.Arce.Interpreter;

namespace DataEditor.Control.Container
{
    public abstract class WrapBaseContainer<TArg> : Control.WrapBaseEditor<FuzzyData.FuzzyObject, TArg>, Control.ObjectContainer
        where TArg : ContainerArgs,new()
    {
        protected ContainerHelper Helper = new ContainerHelper();
        
        public override FuzzyData.FuzzyObject Value
        {
            get { return Helper.ChildValue; }
            set { Helper.ChildValue = value; Pull(); }
        }
        public override FuzzyData.FuzzyObject Parent
        {
            set { Helper.ParentValue = value; Pull(); }
        }
        public override ControlArgs Load_Information (System.Xml.XmlNode Node)
        {
            if ( Binding != null )
            {
                Builder builder = new Builder();
                System.Drawing.Size size = builder.Build(Node, Controls, ExtraWidth, ExtraHeight);
                SetSize(size);
            }
            TArg gba = new TArg();
            gba.Load(Node);
            return gba;
        }
        public override void Pull ()
        {
            if ( Binding != null )
                foreach ( System.Windows.Forms.Control control in Controls )
                    if ( control.Tag != null && control.Tag is ObjectEditor)
                            (control.Tag as ObjectEditor).Parent = Value;
        }
        public override void Push () { /* 已弃用 */ }
        
        public override void Reset ()
        {
            base.Reset();
            if ( argument == null || Binding == null ) return;
            ControlHelper.Reset(this, argument);
            if ( argument.BackColor != default(System.Drawing.Color) )
                Binding.BackColor = argument.BackColor;
            Helper.ChildSymbol = argument.Actual;
            Binding.Text = argument.Text;
        }

        protected virtual int ExtraWidth { get { return 2; } }
        protected virtual int ExtraHeight { get { return 2; } }
        protected virtual void SetSize (System.Drawing.Size size) { Binding.ClientSize = size; }
        protected virtual System.Windows.Forms.Control.ControlCollection Controls
        { get { return Binding != null ? Binding.Controls : null; } }
    }
}
