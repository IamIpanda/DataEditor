using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DataEditor.FuzzyData;

namespace DataEditor.Control.ProtoType.Any
{
    public partial class AnyTypeEditor : UserControl
    {
        /*
         * Array
         * Object
         * Hash
         */
        public readonly static string NEW_VALUE = "（NEW）";
        public readonly static string NAME_VALUE = "（名称）";
        protected FuzzyData.FuzzyObject value;
        public AnyTypeEditor()
        {
            InitializeComponent();
        }

        public FuzzyObject Value
        {
            get { return treeViewForAny1.Value; }
            set { treeViewForAny1.Value = value; }
        }

        private void treeViewForAny1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            listViewForAny1.Value = treeViewForAny1.SelectedObject;
        }

        private void listViewForAny1_DoubleClick(object sender, EventArgs e)
        {
            AnyTypeWindow atw = new AnyTypeWindow();
            atw.Initialize();
            object ob = listViewForAny1.SelectedObject;
            if (ob == null)
            {
                atw.Set(true, NEW_VALUE, FuzzyNil.Instance);
                if (atw.ShowDialog() == DialogResult.OK)
                    Push(atw);
            }
            else
            {
                atw.Set(false, listViewForAny1.SelectedKey.ToString() ,ob);
                if (atw.ShowDialog() == DialogResult.OK)
                    Push(atw);
            }
        }
        protected void Push(AnyTypeWindow atw)
        {
            object ob = treeViewForAny1.SelectedObject;
            Key key = listViewForAny1.SelectedKey;
            if (key == null)
                if (ob is FuzzyArray)
                    (ob as FuzzyArray).Add(atw.Value);
                else if (ob is FuzzyObject)
                {
                    FuzzyObject fo = ob as FuzzyObject;
                    FuzzySymbol fs = FuzzySymbol.GetSymbol(atw.TextName);
                    if (fo.InstanceVariables.ContainsKey(fs))
                        fo.InstanceVariables[fs] = atw.Value;
                    else
                        fo.InstanceVariables.Add(fs, atw.Value);
                }
                else { }
            else
                if (key.Type == Key.KeyType.Array)
                    (ob as FuzzyArray)[key.ArrayKey] = atw.Value;
                else if (key.Type == Key.KeyType.Hash)
                    (ob as FuzzyHash)[key.HashKey] = atw.Value;
                else if (key.Type == Key.KeyType.Object)
                    if (key.ObjectKey.Name == NAME_VALUE)
                        (ob as FuzzyObject).ClassName = FuzzySymbol.GetSymbol(atw.TextValue);
                    else
                        (ob as FuzzyObject).InstanceVariables[key.ObjectKey] = atw.Value;
            listViewForAny1.ScanObject();
        }
    }

    internal class TreeViewForAny : TreeView
    {
        public FuzzyData.FuzzyObject Value
        {
            get { return value; }
            set
            {
                this.value = value;
                Nodes.Clear();
                TreeNode Top = Nodes.Add(Any.GetShortString(value));
                Top.Tag = value;
                ScanObject(value, Top);
            }
        }
        protected FuzzyData.FuzzyObject value;
        public void ScanObject(FuzzyObject Value,TreeNode ParentNode)
        {
             if (Value == null)
                return;
            foreach (FuzzySymbol symbol in Value.InstanceVariables.Keys)
            {
                object Target = Value.InstanceVariables[symbol];
                if (Any.SupportedComplex.Contains(Target.GetType()))
                    ScanObject(Target as FuzzyObject, AddNode("[" + symbol.Name + "]", Target, ParentNode));
            }
            if (Value is FuzzyArray)
            {
                FuzzyArray fa = Value as FuzzyArray;
                for (int i = 0; i < fa.Length; i++)
                {
                    object Target = fa[i];
                    if (Any.SupportedComplex.Contains(Target.GetType()))
                        ScanObject(Target as FuzzyObject, AddNode("[" + i.ToString() + "]", Target, ParentNode));
                }
            }
            if (Value is FuzzyHash)
            {
                FuzzyHash fh = Value as FuzzyHash;
                foreach (object key in fh.Keys)
                {
                    object Target = fh[key];
                    FuzzyObject target = Target as FuzzyObject;
                    if (Any.SupportedComplex.Contains(Target.GetType()))
                        ScanObject(target, AddNode("[" + Any.GetShortString(target) + "]", target, ParentNode));
                }
            }
        }
        protected TreeNode AddNode(string name, object tag, TreeNode parent)
        {
            TreeNode node = new TreeNode(name + Any.GetShortString(tag as FuzzyObject));
            node.Tag = tag;
            parent.Nodes.Add(node);
            return node;
        }
        public FuzzyObject SelectedObject
        {
            get
            {
                if (SelectedNode == null)
                    return null;
                return SelectedNode.Tag as FuzzyObject;
            }
        }
    }
    // TODO : FINISH THIS.
    internal class ListViewForAny : ListView 
    {
        protected FuzzyObject value;
        protected Dictionary<ListViewItem, Key> dictionary = new Dictionary<ListViewItem, Key>();
        public FuzzyObject Value
        {
            get { return value; }
            set 
            {
                this.value = value;
                ScanObject(value);
            }
        }
        public void ScanObject(FuzzyObject Value)
        {
            Items.Clear();
            if (Value == null)
                return;
            dictionary.Add(AddItem(AnyTypeEditor.NAME_VALUE, Value.ClassName), 
                new Key(FuzzySymbol.GetSymbol(AnyTypeEditor.NAME_VALUE), Key.KeyType.Object));
            foreach (FuzzySymbol symbol in Value.InstanceVariables.Keys)
            {
                object Target = Value.InstanceVariables[symbol];
                if (Any.SupportedSingle.Contains(Target.GetType()))
                    dictionary.Add(AddItem(symbol.Name, Target), new Key(symbol, Key.KeyType.Object));
            }
            if (Value is FuzzyArray)
            {
                FuzzyArray fa = Value as FuzzyArray;
                for (int i = 0; i < fa.Length; i++)
                {
                    object Target = fa[i];
                    if (Any.SupportedSingle.Contains(Target.GetType()))
                        dictionary.Add(AddItem("[" + i.ToString() + "]", Target), new Key(i, Key.KeyType.Array));
                }
            }
            if (Value is FuzzyHash)
            {
                FuzzyHash fh = Value as FuzzyHash;
                foreach (object key in fh.Keys)
                {
                    object Target = fh[key];
                    FuzzyObject target = Target as FuzzyObject;
                    if (Any.SupportedSingle.Contains(Target.GetType()))
                        dictionary.Add(AddItem(key.ToString(), target), new Key(key, Key.KeyType.Hash));
                }
            }
            dictionary.Add(AddItem("", null), null);
        }
        public void ScanObject()
        {
            ScanObject(Value);
        }
        protected ListViewItem AddItem(string name, object target)
        {
            TypeManager manager = new TypeManager();
            manager.Value = target;
            string key = name;
            string type = target == null ? "" : "[" + Any.SupportedName[target.GetType()] + "]";
            string value = manager.Text;
            ListViewItem lvi = new ListViewItem(new string[] { key, type, value });
            Items.Add(lvi);
            return lvi;
        }
        public object SelectedObject
        {
            get 
            {
                if (SelectedItems.Count == 0) return null;
                ListViewItem lvi = SelectedItems[0];
                if (lvi == null) return null;
                if (!dictionary.ContainsKey(lvi)) return null;
                Key key = dictionary[lvi];
                if (key == null) return null;
                if (key.Type == Key.KeyType.Array)
                    return (value as FuzzyArray)[key.ArrayKey];
                else if (key.Type == Key.KeyType.Hash)
                    return (value as FuzzyHash)[key.HashKey];
                else
                    return (value as FuzzyObject).InstanceVariables[key.ObjectKey];
            }
        }
        public Key SelectedKey
        {
            get
            {
                if (SelectedItems.Count == 0) return null;
                ListViewItem lvi = SelectedItems[0];
                if (dictionary.ContainsKey(lvi))
                    return dictionary[lvi];
                return null;
            }
        }

    }
    public partial class Any
    {
        public static string GetShortString(FuzzyObject obj)
        {
            if (obj == null) return "";
            return "[" + obj.ClassName.Name + "]";
        }
    }
    internal sealed class Key
    {
        public enum KeyType { Array, Object, Hash };

        private int key1 = -1;
        private FuzzySymbol key2 = null;
        private object key3 = null;
        private KeyType type = KeyType.Array;

        public int ArrayKey { get { return key1; } }
        public FuzzySymbol ObjectKey { get { return key2; } }
        public object HashKey { get { return key3; } }
        public KeyType Type { get { return type; } }

        public Key(object key,KeyType type)
        {
            if (type == KeyType.Array)
                key1 = Convert.ToInt32(key);
            else if (type == KeyType.Hash)
                key3 = key;
            else
                key2 = key as FuzzySymbol;
            this.type = type;
        }
        public override string ToString()
        {
            if (this.type == KeyType.Array)
                return key1.ToString();
            else if (this.type == KeyType.Object)
                return key2.Name;
            else if (this.type == KeyType.Hash)
                return key3.ToString();
            return base.ToString();
        }
    }
}
