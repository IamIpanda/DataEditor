﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.FuzzyData.Serialization.XML
{
    public class FuzzyUserdefinedMarshalDumpObject : FuzzyObject, IXMLUserdefinedMarshalDumpObject
    {
        private object dumpedObject;

        public override string ToString()
        {
            return "#<" + this.ClassName.ToString() + ", dumped object: " + this.dumpedObject.ToString() + ">";
        }

        public object DumpedObject
        {
            get { return dumpedObject; }
            set { dumpedObject = value; }
        }

        public object Dump()
        {
            return this.dumpedObject;
        }
    }
}
