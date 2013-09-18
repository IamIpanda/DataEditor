using System;
using System.Collections.Generic;
using System.Text;
using DataEditor.Contract;
using System.Windows.Forms;

namespace DataEditor.Contract
{
    public abstract class Interpreter
    {
        protected abstract int AddControl(System.Windows.Forms.Control Control,System.Windows.Forms.Control.ControlCollection Collection);
        protected abstract int CalcCoodinate (System.Windows.Forms.Control Control, int Width, int Height);
    }
}