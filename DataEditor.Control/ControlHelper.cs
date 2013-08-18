using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control
{
    static public class ControlHelper
    {
        public static void Reset(ObjectEditor editor, ControlArgs arg)
        {
            System.Windows.Forms.Control control =
                editor as System.Windows.Forms.Control;
            if (control == null) return;
            if (arg.Width != -1)
                control.Width = arg.Width;
            if (arg.Height != -1)
                control.Height = arg.Height;
        }
        public static void OnLeave(object sender, EventArgs e)
        {
            Control.ObjectEditor oe = sender as Control.ObjectEditor;
            if ( oe != null ) oe.Push();
            // 什么都不做
        }
        public static void OnEnter(object sender, EventArgs e)
        {
            // 什么都不做
        }
        public static void OnTaint(object sender, EventArgs e)
        {
            if (sender is System.Windows.Forms.Control) Taint(sender as System.Windows.Forms.Control, Contract.TaintState.Tainted);
        }
        public static class TypeCheck<T> where T : FuzzyData.FuzzyObject
        {
            public static T Get(FuzzyData.FuzzyObject Parent, FuzzyData.FuzzySymbol Actual)
            {
                if (Parent == null || Actual == null) return null;
                if (!Parent.InstanceVariables.ContainsKey(Actual)) return null;
                object target = Parent.InstanceVariables[Actual];
                if (target is T)
                    return (T)target;
                else return null;
            }
        }
        public static void Taint(System.Windows.Forms.Control Control, Contract.TaintState State,int Index = -1)
        {
            ObjectEditor Editor = Control as ObjectEditor;
            // 进行记录
            Help.Log.log("正在将 " + (Editor == null ? "NULL" : Editor.GetType().ToString())
                + " 污染为 " + State.ToString() + " [ 指向：" + Index.ToString() + " ]");
            // 单重可污染结构的场合
            if (Editor is Contract.TaintableSingleEditor)
            {
                Contract.TaintableSingleEditor single = Editor as Contract.TaintableSingleEditor;
                single.Tainted = State;
                Help.TaintRecord.Single[Editor.Value] = State;
                // 若污染状态为“保存”，遍历其子并记为“保存”；
                if (State == Contract.TaintState.Saved)
                {
                    if (Control != null && Control.HasChildren)
                        foreach (System.Windows.Forms.Control Child in Control.Controls)
                            if (Child is Contract.TaintableSingleEditor &&
                                (Child as Contract.TaintableSingleEditor).Tainted != Contract.TaintState.UnTainted)
                                Taint(Child, Contract.TaintState.Saved);
                            else if (Child is Contract.TaintableMultiEditor)
                            {
                                TaintableMultiEditor multi = Child as TaintableMultiEditor;
                                for (int i = 0; i < multi.Tainted.Count; i++)
                                    Taint(Child, Contract.TaintState.Saved, i);
                            }
                }
                // 污染状态不能被标记为“未污染的”
                else if (State == Contract.TaintState.UnTainted) { throw new ArgumentException("Can't taint the data to an untainted state."); }
                // 其他的场合，污染其父为“子污染的”
                else
                {
                    if (Control != null)
                        TaintParent(Control, Contract.TaintState.ChildTainted, Index);
                }
                    
            }
            // 多重可污染结构的场合
            else if (Editor is Contract.TaintableMultiEditor)
            {
                Contract.TaintableMultiEditor multi = Editor as Contract.TaintableMultiEditor;
                if (Index < 0)
                    if (multi is Control.TaintableMultiEditor)
                        (multi as Control.TaintableMultiEditor).SearchTaintValue(Editor.Value);
                    else
                    {
                        if (Help.TaintRecord.Multi[Editor.Value] != null)
                            Help.TaintRecord.Multi[Editor.Value][Index] = State;
                        multi.Tainted[Index] = State;
                    }
                if (Control != null)
                    TaintParent(Control, Contract.TaintState.ChildTainted, Index);
            }
            else
            {
                if (Control != null)
                    TaintParent(Control, Contract.TaintState.ChildTainted, Index);
            }
        }
        private static void TaintParent(System.Windows.Forms.Control Control,Contract.TaintState State,int Index = -1)
        {
            System.Windows.Forms.Control Key = Control.Parent;
            if (Key == null) return;
            while (Key.Parent != null && !(Key is Contract.TaintableEditor))
                Key = Key.Parent;
            if (Key.Parent == null) return;
            Taint(Key, State, Index);
        }
    }
}
