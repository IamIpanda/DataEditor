using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control
{
    public interface TaintableMultiEditor : Contract.TaintableMultiEditor
    {
        void SearchTaintValue(FuzzyData.FuzzyObject Value);
    }
}
