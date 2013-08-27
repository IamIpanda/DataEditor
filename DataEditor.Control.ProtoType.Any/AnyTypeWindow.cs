using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Prototype.Any
{
    public partial class AnyTypeWindow : Form
    {
        public AnyTypeWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            manager.Text = tbValue.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        public bool TextEnabled
        {
            get { return tbName.Enabled; }
            set { tbName.Enabled = value; }
        }
        public string TextName
        {
            get { return tbName.Text; }
            set { tbName.Text = value; }
        }
        public int ValueType
        {
            get { return cbType.SelectedIndex; }
            set { cbType.SelectedIndex = value; }
        }
        public string TextValue
        {
            get { return tbValue.Text; }
            set { tbValue.Text = value; }
        }
        public void Set(bool NameEnabled, string Name, object Value)
        {
            this.Text = "设置 ";
            if (Any.SupportedName.ContainsKey(Value.GetType()))
                this.Text += Any.SupportedName[Value.GetType()];
            tbName.Enabled = NameEnabled;
            tbName.Text = Name;
            if (Any.SuppotedIndex.ContainsKey(Value.GetType()))
                cbType.SelectedIndex = Any.SuppotedIndex[Value.GetType()];
            manager.Value = Value;
            tbValue.Text = manager.Text;
        }

        public void Initialize()
        {
            cbType.Items.Clear();
            foreach (Type type in Any.SupportedSingle)
                cbType.Items.Add(Any.SupportedName[type]);
            cbType.SelectedIndex = -1;
        }

        protected TypeManager manager = new TypeManager();
        public object Value
        {
            get
            {
                return manager.Value;
            }
        }

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbType.SelectedIndex >= 0 && cbType.SelectedIndex < Any.SupportedSingle.Count)
            {
                Type type = Any.SupportedSingle[cbType.SelectedIndex];
                //System.Reflection.Assembly ass = System.Reflection.Assembly.GetAssembly(type);
                //manager.Value = ass.CreateInstance(type.ToString());
                manager.Value = Any.newValue(cbType.SelectedIndex);
               
                tbValue.Text = manager.Text;
            }
        }

    }
}
