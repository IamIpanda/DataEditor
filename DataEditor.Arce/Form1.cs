using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using DataEditor.Contract;
using DataEditor.Help;
using DataEditor.FuzzyData;
using DataEditor.Control.Prototype;

namespace DataEditor.Arce
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load (object sender, EventArgs e)
        {
            Interpreter.Collector.AddAssembly(System.Reflection.Assembly.GetAssembly(typeof(DataEditor.Control.Container.ContainerArgs)));
            Interpreter.Collector.AddAssembly(System.Reflection.Assembly.GetAssembly(typeof(DataEditor.Control.Wrapper.WrapNumInput)));
            Interpreter.Builder builder = new Interpreter.Builder();
            System.Xml.XmlDocument document = new XmlDocument();
            document.Load("Xmls/test1.xml");
            try
            {
                builder.Build(document.FirstChild.NextSibling, this.Controls);
            }
            catch ( Exception exc )
            {
            }
            System.IO.FileStream file = new System.IO.FileStream("Tests/Data/Items.rvdata", System.IO.FileMode.Open);
            System.IO.FileStream filx = new System.IO.FileStream("Tests/Data/CLasses.rvdata", System.IO.FileMode.Open);
            FuzzyObject ob = DataEditor.FuzzyData.Serialization.RubyMarshal.RubyMarshal.Load(file) as FuzzyObject;
            FuzzyObject op = DataEditor.FuzzyData.Serialization.RubyMarshal.RubyMarshal.Load(filx) as FuzzyObject;
            FuzzyArray fa = ob as FuzzyArray;
            FuzzyArray fb = op as FuzzyArray;
            FuzzyObject t = fa[2] as FuzzyObject;
            FuzzyObject s = fb[3] as FuzzyObject;
            FuzzyData.Serialization.XML.XML.Options.IgnoreSymbolEncoding = false;
            DataEditor.FuzzyData.Serialization.XML.XML.Dump(new System.IO.FileStream("x.xml", System.IO.FileMode.Create), s);
            foreach ( System.Windows.Forms.Control control in this.Controls )
            {
                if ( control is TabControl )
                {
                    TabControl tc = control as TabControl;
                    (tc.TabPages[0] as DataEditor.Control.ObjectEditor).Parent = fa;
                    (tc.TabPages[1] as DataEditor.Control.ObjectEditor).Parent = fa;
                    (tc.TabPages[2] as DataEditor.Control.ObjectEditor).Parent = fb;
                }
            }
        }
    }
}