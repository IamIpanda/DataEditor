using System;
using System.Collections.Generic;
using System.Text;
using DataEditor.FuzzyData;

namespace DataEditor.Control
{
    /// <summary>
    /// 适应于 DataEditor.Arce 的 ObjectEditor 契约
    /// </summary>
    public interface ObjectEditor : DataEditor.Contract.ObjectEditor,
        DataEditor.Contract.Iconic//,
        //DataEditor.Contract.TaintableEditor
    {
        /// <summary>
        /// 载入 Node 上的信息，并返回一个适用于此对象的 ControlArg
        /// </summary>
        /// <param name="Node">含有信息的节点</param>
        /// <returns></returns>
        ControlArgs Load_Information(System.Xml.XmlNode Node);
        /// <summary>
        /// 获取或设置此控件的 ControlArg
        /// set 会触发 Reset 方法
        /// </summary>
        ControlArgs Arguments { get; set; }
        /// <summary>
        /// 获取或设置此控件的值。
        /// set 会触发 Pull 方法
        /// </summary>
        FuzzyObject Value { get; set; }
        /// <summary>
        /// 获取或设置此控件的父值
        /// set 会触发 Pull 方法
        /// </summary>
        FuzzyObject Parent { set; }
        /// <summary>
        /// 和此控件绑定的 Label
        /// </summary>
        System.Windows.Forms.Label Label { get; set; }
        /// <summary>
        /// 绑定的实体控件
        /// </summary>
        System.Windows.Forms.Control Binding { get; set; }
    }
}
