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
            get { return Helper.ParentValue; }
            set { Helper.ParentValue = value; Pull(); }
        }
        public override TArg Arguments
        {
            get { return argument; }
            set { argument = value as TArg; Reset(); }
        }
        public override ControlArgs Load_Information (System.Xml.XmlNode Node)
        {
            if ( Binding != null )
            {
                Builder builder = new Builder();
                System.Drawing.Size size = builder.Build(Node, Binding.Controls);
                Binding.ClientSize = size;
            }
            TArg gba = new TArg();
            gba.Load(Node);
            return gba;
        }
        public void Pull ()
        {
            if ( Binding != null )
                foreach ( System.Windows.Forms.Control control in Binding.Controls )
                    if ( control is ObjectEditor )
                        (control as ObjectEditor).Parent = Helper.ChildValue;
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
    }
}
