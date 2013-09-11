using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Window
{
    // TODO : Finish it
    public partial class ArrayChangeDialog : Form
    {
        Adapter.AdvanceCollectNameArray value = null;
        List<FuzzyData.FuzzySymbol> Ints = new List<FuzzyData.FuzzySymbol>();
        List<FuzzyData.FuzzySymbol> Strings = new List<FuzzyData.FuzzySymbol>();
        public ArrayChangeDialog ()
        {
            InitializeComponent();
        }
        public Adapter.AdvanceCollectNameArray Value
        {
            get { return value; }
            set { this.value = value; OnValueChanged(); }
        }
        protected void OnValueChanged ()
        {
            if ( value == null ) return;
            this.Text = "批量更改 " + value.ClassName.Name;
            // 默认：每个同名Object上的内容是相同的。
            FuzzyData.FuzzyObject ob = value.Data[0];
            foreach ( KeyValuePair<FuzzyData.FuzzySymbol, object> properties in ob.InstanceVariables )
                if ( properties.Value is FuzzyData.FuzzyString ) Strings.Add(properties.Key);
                else if ( properties.Value is FuzzyData.FuzzyFloat ) Ints.Add(properties.Key);
                else if ( properties.Value is FuzzyData.FuzzyFixnum ) Ints.Add(properties.Key);
            // ComboBox 推送
            cbmain.Items.Clear();
            foreach ( FuzzyData.FuzzySymbol sym in Ints ) cbmain.Items.Add(sym.Name);
            foreach ( FuzzyData.FuzzySymbol sym in Strings ) cbmain.Items.Add(sym.Name);
        }

        private void tbmain_TextChanged (object sender, EventArgs e)
        {

        }
    }
}
