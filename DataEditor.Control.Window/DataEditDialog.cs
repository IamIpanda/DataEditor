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

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            FuzzyData.FuzzyObject temp = origin & value;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        protected FuzzyData.FuzzyObject value, origin;
        public FuzzyData.FuzzyObject Value
        {
            get { return GetParent(); }
            set
            {
                if (value != null)
                {
                    this.origin = value;
                    this.value = value.Clone() as FuzzyData.FuzzyObject;
                    SetParent();
                }
            }
        }

        protected FuzzyData.FuzzyObject GetParent()
        {
            if (value != simpleContainer1.Parent)
                SetParent();
            return value;
        }
        protected void SetParent()
        {
            simpleContainer1.Parent = value;
        }

        public void Load_Information(System.Xml.XmlNode Node)
        {
            ControlArgs arg = simpleContainer1.Load_Information(Node);
            simpleContainer1.Arguments = arg;  
        }

    }
    
    public class DataEditorDialog : DataEditDialog, DataEditor.Control.ComplexContainer
    {
        DataEditor.Control.Container.ContainerHelper Helper =
            new Container.ContainerHelper();
        EditorWindowArgs argument;
        public string Flag { get { return "e-window"; } }
        public Label Label { get; set; }
        public FuzzyData.FuzzyObject Value
        {
            get { return Helper.ChildValue; }
            set { Helper.ChildValue = value; Pull(); }
        }
        public new FuzzyData.FuzzyObject Parent
        {
            get { return Helper.ParentValue; }
            set { Helper.ParentValue = value; Pull(); }
        }
        public ControlArgs Arguments
        {
            get { return argument; }
            set { argument = value as EditorWindowArgs; Reset(); }
        }
        public new ControlArgs Load_Information(System.Xml.XmlNode Node)
        {
            DataEditor.Arce.Interpreter.Builder builder = new Arce.Interpreter.Builder();
            System.Drawing.Size size = builder.Build(Node, simpleContainer1.Controls, 2, 13);
            this.SetClientSizeCore(size.Width, size.Height);
            EditorWindowArgs gba = new EditorWindowArgs();
            gba.Label = 0;
            gba.Load(Node);
            return gba;
        }
        public void Pull()
        {
            foreach (System.Windows.Forms.Control control in this.Controls)
                if (control is ObjectEditor)
                    (control as ObjectEditor).Parent = Helper.ChildValue;
        }
        public void Push() { }
        public void Reset()
        {
            System.Windows.Forms.MessageBox.Show(this.ClientRectangle.ToString());
            if (argument == null) return;
            ControlHelper.Reset(this, argument);
            if (argument.BackColor != default(System.Drawing.Color))
                this.BackColor = argument.BackColor;
            Helper.ChildSymbol = argument.Actual;
            this.Text = argument.Text;
        }
    }
    public class EditorWindowArgs : DataEditor.Control.Container.ContainerArgs
    {
        
    }
}