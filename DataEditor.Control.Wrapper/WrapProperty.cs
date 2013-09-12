using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Wrapper
{
    public class WrapProperty : WrapBaseEditor<Adapter.AdvanceCollectNameArray ,PropertyArgs>
    {
        Prototype.ProtoListView actual;
        System.Xml.XmlNode dialog;
        FuzzyData.FuzzyObject New;
        FuzzyData.FuzzySymbol code;
        List<PropertyArgs.PropertyTextArgs> columnArgs;
        List<FuzzyData.FuzzySymbol> parameters;
        List<Help.AnotherTextManager[]> texts = new List<Help.AnotherTextManager[]>();
        public override string Flag { get { return "property"; } }
        public override void Push () { }
        public override void Pull ()
        {
            if ( value != null ) ShowText();
        }
        public override void Bind ()
        {
            Binding = actual = new Prototype.ProtoListView();
            actual.DoubleClick += actual_DoubleClick;
        }

        void actual_DoubleClick (object sender, EventArgs e)
        {
        }
        public override void Reset ()
        {
            base.Reset();
            if ( argument == null ) return;
            dialog = argument.Dialog;
            New = argument.New;
            columnArgs = argument.Columns;
            parameters = argument.Parameters;
            code = argument.Code;
            if ( actual == null ) return;
            foreach ( var row in argument.Columns )
            {
                int w = row.Width == -1 ? 200 : row.Width;
                actual.Columns.Add(row.Text, w);
            }
        }

        protected override Adapter.AdvanceCollectNameArray ConvertToValue (FuzzyData.FuzzyObject origin)
        {
            FuzzyData.FuzzyArray arr = origin as FuzzyData.FuzzyArray;
            if ( arr == null ) return null;
            Adapter.AdvanceCollectNameArray array = new Adapter.AdvanceCollectNameArray(arr);
            return array.Exists ? array : null;
        }

        protected object[] GetParameters (FuzzyData.FuzzyObject parent = null)
        {
            if ( parent == null ) parent = SelectedValue;
            if ( parent == null ) return null;
            List<object> paras = new List<object>();
            foreach ( FuzzyData.FuzzySymbol sym in parameters )
                paras.Add(GetParameter(parent, sym));
            return paras.ToArray();
        }
        protected object GetParameter (FuzzyData.FuzzyObject Parent, FuzzyData.FuzzySymbol Sym)
        {
            if ( Parent == null || Sym == null ) return null;
            object value = null;
            Parent.InstanceVariables.TryGetValue(Sym, out value);
            return value;
        }
        public FuzzyData.FuzzyObject SelectedValue
        {
            get 
            {
                if ( actual == null ) return null;
                if ( actual.SelectedItems.Count == 0 ) return null;
                if ( value == null ) return FuzzyData.FuzzyNil.Instance;
                return value.Data[actual.SelectedIndices[0]];
            }
        }
        public void ShowText ()
        {
            texts.Clear();
            int count = columnArgs.Count;
            foreach ( FuzzyData.FuzzyObject ob in value.Data )
            {
                // 列表生成
                Help.AnotherTextManager[] ts = new Help.AnotherTextManager[count];
                // 抓取CODE
                FuzzyData.FuzzyFixnum fixnum_code = GetParameter(ob, code) as FuzzyData.FuzzyFixnum;
                // 若获取 CODE 失败
                if ( fixnum_code == null )
                {
                    ts[0] = new Help.AnotherTextManager();
                    ts[0].Initialize("!NO CODE");
                    for ( int i = 1; i < count; i++ ) ts[i] = new Help.AnotherTextManager();
                }
                // 若获取 CODE 成功
                else
                {
                        int CODE = Convert.ToInt32(fixnum_code.Value);
                        for ( int i = 0; i < count; i++ )
                        {
                            ts[i] = new Help.AnotherTextManager();
                            ts[i].Initialize(columnArgs[i][CODE], GetParameters(ob));
                        }
                    
                }
                texts.Add(ts);
            }
            PushText();
        }
        public void PushText()
        {
            actual.Items.Clear();
            foreach ( Help.AnotherTextManager[] managers in texts )
            {
                string[] ss = new string[managers.Length];
                for ( int i = 0; i < managers.Length; i++ )
                    ss[i] = managers[i].ToString();
                actual.Items.Add(new ListViewItem(ss));
            }
        }
    }
   public class PropertyArgs : ListViewArgs
   {
       public FuzzyData.FuzzySymbol Code { get; set; }
       public List<FuzzyData.FuzzySymbol> Parameters { get; set; }
       public new List<PropertyTextArgs> Columns { get; set; }

       public PropertyArgs ()
       {
           Code = null;
           Parameters = new List<FuzzyData.FuzzySymbol>();
           Columns = new List<PropertyTextArgs>();
       }
       public PropertyArgs (System.Xml.XmlNode Node) : this() { Load(Node); }
       public override void Load (System.Xml.XmlNode Node)
       {
           base.OriginLoad(Node);
       }

       protected override void OnScan (string Name, string InnerText)
       {
           if ( Name == "CODE" ) Code = GetSymbol(InnerText);
           else if ( Name == "PARAMETER" ) Parameters.Add(GetSymbol(InnerText));
           base.OnScan(Name, InnerText);
       }

       protected override void OnCheck (string Name, System.Xml.XmlNode Node)
       {
           if ( Name == "COLUMN" ) Columns.Add(new PropertyTextArgs(Node));
           else if ( Name == "NEW" ) New = LoadXmlData(Node.FirstChild);
           else if ( Name == "DIALOG" ) Dialog = Node;
           base.OnCheck(Name, Node);
       }
       public class PropertyTextArgs : PropertyArgs.ListViewColumnArgs
       {
           public  new  Dictionary<int, string> Format { get; set; }
           public bool AllString { get; set; }
           public PropertyTextArgs () { Format = new Dictionary<int, string>(); AllString = false; }
           public PropertyTextArgs (System.Xml.XmlNode Node) : this() { Load(Node); }
           protected override void OnScan (string Name, string InnerText)
           {
               if ( Name == "STRING" ) AllString = !AllString;
               base.OnScan(Name, InnerText);
           }
           protected override void OnCheck (string Name, System.Xml.XmlNode Node)
           {
               if ( Name == "TEXTS" )
               {
                   foreach ( System.Xml.XmlNode child in Node.ChildNodes )
                       if ( child.Name.ToUpper() == "TEXT" )
                       {
                           int index = -1;
                           foreach ( System.Xml.XmlAttribute attr in child.Attributes )
                               if ( attr.Name.ToUpper() == "CODE" )
                                   index = GetInt(attr.InnerText);
                           if ( index > 0 )
                               Format.Add(index, child.InnerText);
                       }
               }
               base.OnCheck(Name, Node);
           }
           public string this[int i]
           {
               get
               {
                   string s = "# NO SUCH CODE : " + i.ToString() + " #";
                   Format.TryGetValue(i, out s);
                   return s;
               }
           }
           public FuzzyData.FuzzySymbol Code { get; set; }
           public string this[FuzzyData.FuzzyObject ob,FuzzyData.FuzzySymbol sym]
           {
               get 
               {
                   FuzzyData.FuzzyFixnum code = 
                       ControlHelper.TypeCheck<FuzzyData.FuzzyFixnum>.Get(ob, sym);
                   if ( code == null ) return "# DISABLE CODE #";
                   int num = Convert.ToInt32(code.Value);
                   return this[num];
               }
           }
           public string this[FuzzyData.FuzzyObject ob]
           {
               get { return this[ob, Code]; }
           }
       }
   }
}
