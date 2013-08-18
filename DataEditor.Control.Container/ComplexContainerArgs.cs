using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Container
{
    /// <summary>
    /// 为 ComplexContainerArgs 提供参数的类型。
    /// 我们约定：
    /// Value 和 Container 的 Parent 是一致的。
    /// Value 的符号用 Real 属性标记。
    /// Parent 的符号用 Actual 属性标记。
    /// </summary>
    public class ComplexContainerArgs : ContainerArgs
    {
        public FuzzyData.FuzzySymbol Real { get; set; }
        public ComplexContainerArgs() : base()
        {
            this.Real = null;
        }
        public ComplexContainerArgs (System.Xml.XmlNode node) : this() { Load(node); }
        protected override void OnScan(string Name, string InnerText)
        {
            base.OnScan(Name, InnerText);
            if (Name == "REAL")
            {
                string name = InnerText;
                if (!name.StartsWith("@")) name = "@" + name;
                this.Real = FuzzyData.FuzzySymbol.GetSymbol(name);
            }
        }
    }
}
