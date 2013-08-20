using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control
{
    public interface TaintableMultiEditor
    {
        void SearchTaintValue(FuzzyData.FuzzyObject Value);
    }
}
