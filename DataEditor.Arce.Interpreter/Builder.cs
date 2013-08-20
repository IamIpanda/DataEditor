using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Drawing;
using System.Windows.Forms;

namespace DataEditor.Arce.Interpreter
{
    public class Builder : Interpreter
    {
        /// <summary>
        /// 关于控件走向的顺序。
        /// Row 表示 控件 自上向下排布
        /// Column 表示控件 自左向右排布。
        /// </summary>
        public enum ControlOrder { Row, Column };
        protected int max_x = 0, max_y = 0;
        protected int now_x = 0, now_y = 0;
        protected int now_w = 0, now_h = 0;
        protected int head_x = 0, head_y = 0;
        protected ControlOrder mode = ControlOrder.Row;
        /// <summary>
        /// 根据给定的 TopNode，“建造” 一个空间环境
        /// </summary>
        /// <remarks>
        /// 约定：所有的数据节点，都位于且仅位于 TopNode 的所有直接子节点下。
        /// 就理论上而言，TopNode 的 Name 应当为 Window，但不会检查它。
        /// 硬性规定：Page 成为一个子节点。
        /// 在节点下，可以使用 FILE 节点标记另一个文件内的节点内容。
        /// </remarks>
        /// <param name="TopNode">含有标记内容的 Xml 节点</param>
        /// <param name="Collection">控件包</param>
        /// <param name="default_head_x">默认的 x 轴起始位置</param>
        /// <param name="defaule_head_y">默认的 y 轴起始位置</param>
        /// <returns>建造完毕后，得出的所有控件的尺寸</returns>
        public Size Build(System.Xml.XmlNode TopNode, System.Windows.Forms.Control.ControlCollection Collection,Control.ObjectEditor Parent = null, int default_head_x = 0, int defaule_head_y = 0)
        {
            // 把所有变量初始化。
            max_x = head_x = now_x = default_head_x;
            max_y = head_y = now_y = defaule_head_y;
            string NodeName, ControlName;
            foreach (XmlNode ChildNode in TopNode.ChildNodes)
            {
                NodeName = ChildNode.Name;
                // 若为控制指令，则执行并计算下一条指令。
                if (CheckControlNode(ChildNode))
                    continue;
                // 若为控件指令，则生成它。
                if (ChildNode.Name.ToUpper() == "DATA")
                {
                    // 查找控件名
                    ControlName = "";
                    foreach (XmlAttribute attr in ChildNode.Attributes)
                        if (attr.Name.ToUpper() == "TYPE")
                            ControlName = attr.InnerText;
                    // 生成此控件
                    DataEditor.Control.ObjectEditor editor =
                        Collector.Instance[ControlName] as DataEditor.Control.ObjectEditor;
                    // 若生成失败，此节点被送往一个空方法
                    if (editor == null)
                    { FailedBuildingNodes(ChildNode); continue; }
                    // 生成函数参数结构体，并将此结构体上传给函数
                    // 若此控件是一个容器，则在生成函数参数结构体的同时，应当已经在内部进行了子控件生成。
                    DataEditor.Control.ControlArgs argument = editor.Load_Information(ChildNode);
                    editor.Arguments = argument;
                    // 标记其父编辑器
                    editor.Container = Parent;
                    // 转换成控件形式
                    System.Windows.Forms.Control control = editor.Binding;
                    // 若转换失败，依然将节点送往一个空方法
                    if (control == null)
                    { FailedBuildingNodes(ChildNode); continue; }
                    // 将事件绑定给控件
                    control.Enter += DataEditor.Control.ControlHelper.OnEnter;
                    control.Leave += DataEditor.Control.ControlHelper.OnLeave;
                    // 留下追踪信息
                    control.Tag = editor;
                    // 生成和计算 Label 的影响
                    int extra_w = 0, extra_h = 0;
                    Label label = GetLabel(now_x, now_y, argument.Text);
                    int label_mode = argument.Label;
                    {
                        switch (label_mode)
                        {
                            // Label 放在上边
                            case 1:
                                extra_h = control.Margin.Top + label.Height; break;
                            // Label 放在左边
                            case 2:
                                extra_w = control.Margin.Left + label.Width; break;
                        }
                    }
                    control.Location = new Point(now_x + extra_w, now_y + extra_h);
                    // 若以上结算全部成功，计算它的坐标值
                    CalcCoodinate(control, control.Width + extra_w, control.Height + extra_h);
                    // 设置标签
                    if (label_mode != 0)
                    {
                        editor.Label = label;
                        AddLabel(label, Collection);
                    }
                    // 上传到父控件中
                    AddControl(control, Collection);
                }
                // 此后，剩余的节点被送往一个空方法。
                else NotDefinedNodes(ChildNode);
            }
            return new Size(max_x, max_y);
        }
        /// <summary>
        /// 将控件上传到父控件
        /// </summary>
        /// <param name="Control"></param>
        /// <param name="Collection"></param>
        /// <returns></returns>
        protected override int AddControl(System.Windows.Forms.Control Control, System.Windows.Forms.Control.ControlCollection Collection)
        {
                Collection.Add(Control);
                return 1;
        }
        /// <summary>
        /// 将 Label 上传到父控件
        /// </summary>
        /// <param name="Label"></param>
        /// <param name="Collection"></param>
        /// <returns></returns>
        protected virtual int AddLabel(Label Label, System.Windows.Forms.Control.ControlCollection Collection)
        {
            try
            {
                Collection.Add(Label);
            }
            catch (Exception ex)
            {
            }
            return 1;
        }
        /// <summary>
        /// 结算此控件的坐标
        /// </summary>
        /// <param name="Control">要结算的控件</param>
        /// <param name="w">控件的合宽</param>
        /// <param name="h">控件的合高</param>
        /// <returns></returns>
        protected override int CalcCoodinate(System.Windows.Forms.Control Control, int w, int h)
        {
            if (now_x + w > max_x)
                max_x = now_x + w;
            if (now_y + h > max_y)
                max_y = now_y + h;
            if (mode == ControlOrder.Row)
            {
                now_y += h + Control.Margin.Bottom;
                if (w > now_w) now_w = w;
            }
            else
            {
                now_w += w + Control.Margin.Right;
                if (h > now_h) now_h = h;
            }
            return 1;
        }
        /// <summary>
        /// 根据给定的数据，生成并返回一个 Label
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        protected virtual Label GetLabel(int x, int y, string text)
        {
            Label l = new Label();
            l.Location = new Point(x, y);
            l.Text = text;
            l.Size = l.PreferredSize;
            return l;
        }
        protected virtual void NotDefinedNodes(XmlNode Node) {  }
        protected virtual void FailedBuildingNodes(XmlNode Node) { }
        /// <summary>
        /// 检查节点是否为控制节点。若是，则返回值为真，并执行那个控制指令。
        /// </summary>
        /// <param name="Node"></param>
        /// <returns></returns>
        protected virtual bool CheckControlNode(XmlNode Node)
        {
            if (Node.Name.ToUpper() == "ORDER")
            { RunOrder(Node.InnerText); return true; }
            else if (Node.Name.ToUpper() == "SPACE")
            { RunSpace(Node.InnerText); return true; }
            else if (Node.Name.ToUpper() == "NEXT")
            { RunSpace(Node.InnerText); return true; }
            else if (Node.Name.ToUpper() == "FILE")
            { RunFile(Node.InnerText); return true; }
                
            else if ( Node.Name.ToUpper() == "CONVERTS" )
            { RunConvert(Node); return true; }
            else if ( Node.Name.ToUpper() == "CONVERTF" )
            { RunConvertFromFile(Node.InnerText); return true; }
            return false;
        }
        protected virtual void RunOrder(string InnerText)
        {
            if (InnerText == "0")
                mode = ControlOrder.Row;
            else if (InnerText == "1")
                mode = ControlOrder.Column;
            else
                mode = mode == ControlOrder.Row ? ControlOrder.Column : ControlOrder.Row;
        }
        protected virtual void RunSpace(string InnerText)
        {
            int space = 20;
            int.TryParse(InnerText,out space);
            if (mode == ControlOrder.Row)
                now_y += space;
            else
                now_x += space;
        }
        protected virtual void RunNext(string InnerText)
        {
            if (mode == ControlOrder.Row)
            {
                now_y = head_y;
                head_x = now_x + now_w;
                now_x = head_x;
                now_w = 0;
            }
            else
            {
                now_x = head_x;
                head_y = now_y + now_h;
                now_y = head_y;
                now_h = 0;
            }
        }
        // TODO：Finish this
        protected virtual void RunFile(string InnerText)
        {
            string str = Help.PathHelper.CheckName(InnerText);
            if (str == "" || str == null) Help.Log.log("无法找到此文件：" + InnerText);
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(InnerText);
            }
            catch
            {
                Help.Log.log("无法加载此文件：" + InnerText);
                return;
            }
            XmlNode MainNode = document.FirstChild.NextSibling;
            // TODO Get enough Information.
        }
        protected virtual void RunConvert (XmlNode Node)
        {
            Help.NounConverter.Load(Node);   
        }
        protected virtual void RunConvertFromFile (string InnerText)
        {
            Help.NounConverter.Load(InnerText);
        }
    }
}
