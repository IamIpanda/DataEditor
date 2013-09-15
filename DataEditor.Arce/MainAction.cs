using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Arce
{
    public partial class MainAction : Control.Window.SimpleRadioChoose
    {
        public MainAction ()
        {
            InitializeComponent();
            ActionCore.Run();
            base.lbDescription.Text = "您想要做什么？";
            Help.MainMenuCommand.Sort();
            foreach ( var command in Help.MainMenuCommand.Commands )
                base.protoListBox1.Items.Add(command.Name);
        }
    }
}
