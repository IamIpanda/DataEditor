﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Prototype
{
    public partial class ProtoRadioContainer : UserControl
    {
        public ProtoRadioContainer()
        {
            InitializeComponent();
        }
        public new string Text
        {
            get { return radioButton1.Text; }
            set { radioButton1.Text = value; }
        }

        private void radioButton1_SizeChanged(object sender, EventArgs e)
        {
            ResetRadio();
        }

        private void panel2_SizeChanged(object sender, EventArgs e)
        {
            ResetRadio();
        }

        protected void ResetRadio()
        {
            radioButton1.Location = new Point(
                (panel2.Width - radioButton1.Width) / 2,
                (panel2.Height - radioButton1.Height) / 2);
        }

        public RadioButton Radio { get { return radioButton1; } }

        public new System.Windows.Forms.Control.ControlCollection Controls { get { return panel1.Controls; } }
    }
    public class RadioGroup
    {
        string Key;
        Dictionary<string, RadioButton> radios = new Dictionary<string, RadioButton>();
        void CheckedChanged(object sender, EventArgs e)
        {
            RadioButton r = sender as RadioButton;
            if (r == null || r.Checked == false) return;
            foreach (RadioButton rb in radios.Values)
                if (rb != null && rb != r) rb.Checked = false;
        }
        public void AddKey(string key)
        {
            radios.Add(key, null);
        }
        public void AddRadio(string key, RadioButton radio)
        {
            if (radios.ContainsKey(key))
                radios[key] = radio;
            else Help.Log.log("没有被匹配的Key：" + key);
        }
        public bool this[string key]
        {
            get { return radios.ContainsKey(key); }
        }
        protected RadioGroup(string Key) { this.Key = Key; }
        protected static List<RadioGroup> Groups = new List<RadioGroup>();
        public static RadioGroup Create(string Key) { RadioGroup g = new RadioGroup(Key); Groups.Add(g); return g; }
        public static void AddRadios(string key,RadioButton radio)
        {
            foreach (RadioGroup group in Groups)
                if (group[key]) group.AddRadio(key, radio);
        }
    }
}
