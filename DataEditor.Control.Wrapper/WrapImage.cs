using System;
using System.Collections.Generic;
using System.Text;
using DataEditor.Adapter;

namespace DataEditor.Control.Wrapper
{
    public class WrapImage
    {

    }
    public class ImageArgs : ControlArgs
    {
        protected List<AdvanceImage.AdvanceImageRect> Splits = new List<AdvanceImage.AdvanceImageRect>();
        protected List<AdvanceImage.AdvanceImageRect> Indecis = new List<AdvanceImage.AdvanceImageRect>();

    }
}
