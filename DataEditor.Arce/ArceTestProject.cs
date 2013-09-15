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
    public partial class ArceTestProject : Form
    {
        public ArceTestProject()
        {
            InitializeComponent();
        }

        private void Form1_Load (object sender, EventArgs e)
        {
            new MainAction().ShowDialog();
            Interpreter.Collector.AddAssembly(System.Reflection.Assembly.GetAssembly(typeof(DataEditor.Control.Container.ContainerArgs)));
            Interpreter.Collector.AddAssembly(System.Reflection.Assembly.GetAssembly(typeof(DataEditor.Control.Wrapper.WrapNumInput)));
            Interpreter.Builder builder = new Interpreter.Builder();
            System.Xml.XmlDocument document = new XmlDocument();
            document.Load("Xmls/test1.xml");
            try
            {   
                builder.Build(document.FirstChild.NextSibling, panel1.Controls);
            }
            catch ( Exception exc )
            {
            }
            System.IO.FileStream file = new System.IO.FileStream("Tests/Data/Items.rvdata", System.IO.FileMode.Open);
            System.IO.FileStream filx = new System.IO.FileStream("Tests/Data/Classes.rvdata", System.IO.FileMode.Open);
            System.IO.FileStream fily = new System.IO.FileStream("Tests/Data/Actors.rvdata2", System.IO.FileMode.Open);
            System.IO.FileStream filz = new System.IO.FileStream("Tests/Data/System.rvdata2", System.IO.FileMode.Open);

            FuzzyObject ob = DataEditor.FuzzyData.Serialization.RubyMarshal.RubyMarshal.Load(file) as FuzzyObject;
            FuzzyObject op = DataEditor.FuzzyData.Serialization.RubyMarshal.RubyMarshal.Load(filx) as FuzzyObject;
            FuzzyObject od = DataEditor.FuzzyData.Serialization.RubyMarshal.RubyMarshal.Load(fily) as FuzzyObject;
            FuzzyObject oq = DataEditor.FuzzyData.Serialization.RubyMarshal.RubyMarshal.Load(filz) as FuzzyObject;
            FuzzyArray fa = ob as FuzzyArray;
            FuzzyArray fb = op as FuzzyArray;
            FuzzyArray fc = od as FuzzyArray;

            foreach ( System.Windows.Forms.Control control in panel1.Controls )
            {
                if ( control is TabControl)
                {
                    TabControl tc = control as TabControl;
                    try
                    {
                        (tc.TabPages[0].Tag as DataEditor.Control.ObjectEditor).Parent = fa;
                        (tc.TabPages[1].Tag as DataEditor.Control.ObjectEditor).Parent = fb;
                        (tc.TabPages[2].Tag as DataEditor.Control.ObjectEditor).Parent = oq;
                        (tc.TabPages[3].Tag as DataEditor.Control.ObjectEditor).Parent = fc;
                    }
                    catch ( Exception ex )
                    {
                    }
                }
            }
        }
    }
}