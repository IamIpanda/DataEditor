using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Wrapper
{
    public class WrapChooseBox : Prototype.ProtoComboBox, DataEditor.Control.ObjectEditor,Contract.TaintableSingleEditor
    {
        ChooseBoxArgs argument;
        FuzzyData.FuzzyFixnum value;
        FuzzyData.FuzzySymbol key;
        Contract.TaintState taint;
        Help.LinkTable<int, int> Link = new Help.LinkTable<int,int>();
        public ControlArgs Load_Information(System.Xml.XmlNode Node) { return new ChooseBoxArgs(Node); }
        public string Flag { get { return "choose"; } }
        public Label Label { get; set; }
        public ControlArgs Arguments
        {
            get { return argument; }
            set { argument = value as ChooseBoxArgs; Reset(); }
        }
        public FuzzyData.FuzzyObject Value
        {
            get { return value; }
            set { this.value = value as FuzzyData.FuzzyFixnum; Pull(); }
        }
        public Contract.TaintState Tainted
        {
            get { return taint; }
            set { taint = value; Putt(); }
        }
        public new FuzzyData.FuzzyObject Parent
        {
            set
            {
                FuzzyData.FuzzyFixnum num = ControlHelper.TypeCheck<FuzzyData.FuzzyFixnum>.Get(value, key);
                if (num != null) Value = num;
                else if (Label != null) this.Enabled = false;
                Pull();
            }
        }
        public WrapChooseBox()
        {
            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        public void Push()
        {
            if (value != null)
                value.Value = Link.Reverse[this.SelectedIndex];
        }
        public void Reset()
        {
            if (argument == null) return;
            this.Enabled = true;
            DataEditor.Control.ControlHelper.Reset(this, argument);
            this.key = argument.Actual;
            Link.Clear();
            Items.Clear();
            // id for i,index for j
            int i = -1, j = 0;
            foreach (var manager in argument.List)
            {
                i++;
                if (manager == null) continue;
                Link.Add(i, j++);
                Items.Add(manager);
            }
        }
        public void Pull()
        {
            if (value != null)
            {
                this.SelectedIndex = Link.Verse[(int)value.Value];
                Tainted = Help.TaintRecord.Single[value];
            }
        }
        public void Putt()
        {
            Help.TaintHelper.OnPutt(Label, taint);
        }
    }
    public class ChooseBoxArgs : ControlArgs
    {
        protected Help.DefaultList<Help.TextManager> StringList = new Help.DefaultList<Help.TextManager>();
        public ChooseBoxArgs()
            : base()
        {

        }
        public ChooseBoxArgs(System.Xml.XmlNode Node)
            : base(Node)
        {
 
        }
        public override void Load(System.Xml.XmlNode Node)
        {
            base.Load(Node);
            StringList.Clear();
            foreach (System.Xml.XmlNode ChildNode in Node.ChildNodes)
            {
                if (ChildNode.Name.ToUpper() == "CHOICE")
                {
                    string Name;
                    string Text = GetValue(ChildNode);
                    string File = null;
                    string ID = "";
                    foreach (System.Xml.XmlAttribute attr in ChildNode.Attributes)
                    {
                        Name = attr.Name.ToUpper();
                        if (Name == "TEXT") Text = attr.InnerText;
                        else if (Name == "FILE") File = attr.InnerText;
                        else if (Name == "ID") ID = attr.InnerText;
                    }
                    if (File != null && File != "")
                    {
                        // TODO : Finish this.
                        Help.FileArrayManager file = Help.FileArrayManager.Create(File);
                        foreach (FuzzyData.FuzzyObject obj in file.Value)
                        {
                            object id = Help.PathHelper.LoadChild(obj, ID);
                            int OwnID = -1;
                            if (id is int) OwnID = Convert.ToInt32(id);
                            else if (id is FuzzyData.FuzzyFixnum) OwnID =
                                Convert.ToInt32((id as FuzzyData.FuzzyFixnum).Value);
                            StringList[OwnID] = new Help.TextManager(Text, obj);
                        }
                    }
                    else
                    {
                        int id = GetInt(ID);
                        StringList[id] = new Help.TextManager(Text);
                    }
                }
            }
        }
        public Help.DefaultList<Help.TextManager> List { get { return StringList; } }
    }
}
