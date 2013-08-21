using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Wrapper
{
    public class WrapChooseBox : Control.WrapBaseEditor<FuzzyData.FuzzyFixnum,ChooseBoxArgs>
    {
        Prototype.ProtoComboBox actual;
        Help.LinkTable<int, int> Link = new Help.LinkTable<int,int>();
        public override string Flag { get { return "choose"; } }
        public override void Bind ()
        {
            var combo = new Prototype.ProtoComboBox();
            combo.DropDownStyle = ComboBoxStyle.DropDownList;
            Binding = combo;
            actual = combo;
        }
        public override void Push()
        {
            if (value != null && actual != null)
                value.Value = Link.Reverse[actual.SelectedIndex];
        }
        public override void Reset()
        {
            base.Reset();
            if ( argument == null ) return;
            Link.Clear();
            actual.Items.Clear();
            // id for i,index for j
            int i = -1, j = 0;
            foreach (var manager in argument.List)
            {
                i++;
                if (manager == null) continue;
                Link.Add(i, j++);
                actual.Items.Add(manager);
            }
        }
        public override void Pull()
        {
            if (value != null && actual != null)
                actual.SelectedIndex = Link.Verse[(int)value.Value];
        }
        protected override bool CheckValue ()
        {
            if ( actual == null || value == null ) return false;
            return !(value.Value == Link.Reverse[actual.SelectedIndex]);
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
                        if ( file.Exists == false ) return;
                        foreach (FuzzyData.FuzzyObject obj in file.Value.Data)
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
