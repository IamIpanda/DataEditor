using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace DataEditor.Control.Window
{
    public partial class DataEditDialog : Form
    {
        public DataEditDialog()
        {
            InitializeComponent();
        }

        protected virtual void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        protected virtual void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
    
    public class DataEditorDialog : DataEditDialog, DataEditor.Control.ObjectContainer
    {
        DataEditor.Control.Container.ContainerHelper Helper =
            new Container.ContainerHelper();
        EditorWindowArgs argument;
        public string Flag { get { return "e-window"; } }
        public Label Label { get; set; }
        protected FuzzyData.FuzzyObject origin;
        public FuzzyData.FuzzyObject Value
        {
            get { return Helper.ChildValue; }
            set { Helper.ChildValue = value.Clone() as FuzzyData.FuzzyObject; this.origin = value; Pull(); }
        }
        public new FuzzyData.FuzzyObject Parent
        {
            get { return Helper.ParentValue; }
            set { Helper.ParentValue = value.Clone() as FuzzyData.FuzzyObject; this.origin = value ; Pull(); }
        }
        public ControlArgs Arguments
        {
            get { return argument; }
            set { argument = value as EditorWindowArgs; simpleContainer1.Arguments = value as EditorWindowArgs; Reset(); }
        }
        public ControlArgs Load_Information(System.Xml.XmlNode Node)
        {
            DataEditor.Arce.Interpreter.Builder builder = new Arce.Interpreter.Builder();
            System.Drawing.Size size = builder.Build(Node, simpleContainer1.Controls, 2, 3);
            int width = size.Width + 30;
            int height = size.Height + 40;
            if ( width < 200 ) width = 200;
            this.SetClientSizeCore(width, height);
            EditorWindowArgs gba = new EditorWindowArgs();
            gba.Label = 0;
            gba.Load(Node);
            return gba;
        }
        public void Pull()
        {
            this.simpleContainer1.Parent = Helper.ChildValue;
        }
        public void Push() { /* 已弃用 */ }
        public void Reset()
        {
            if (argument == null) return;
            ControlHelper.Reset(this, argument);
            if (argument.BackColor != default(System.Drawing.Color))
                this.BackColor = argument.BackColor;
            Helper.ChildSymbol = argument.Actual;
            this.Text = argument.Text;
            this.simpleContainer1.Reset();
        }
        protected override void button1_Click(object sender, EventArgs e)
        {
            origin &= Helper.ChildValue;
            base.button1_Click(sender, e);
        }
        
    }
    public class EditorWindowArgs : DataEditor.Control.Container.SimpleBoxArgs
    {
        public EditorWindowArgs() : base() { }
        public EditorWindowArgs(System.Xml.XmlNode node) : base(node) { }
        public EditorWindowArgs(Container.SimpleBoxArgs arg)
        {
            this.Actual = arg.Actual;
            this.BackColor = arg.BackColor;
            this.Height = arg.Height;
            this.Label = 0;
            this.Text = arg.Text;
            this.Width = arg.Width;
        }
    }
}