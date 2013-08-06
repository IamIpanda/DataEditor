using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using DataEditor.FuzzyData;

// 重构！

namespace DataEditor.Help
{
    public class TextManager
    {
        string[] partial;
        List<Factor> objects = new List<Factor>();
        public event EventHandler TextChanged;

        static protected string KeyChar = '\0'.ToString();
        static protected Regex Reg = new Regex("#({(.+?)}){0,1}#");
        // TODO : REREAD THEM AND EXCHANGE;
        public TextManager(string Format, params object[] args)
        {
            // 字符串拆分
            string[] formats = Format.Split(',');
            // 对于第一部分，拆分并依次获得格式信息
            string temp = Reg.Replace(formats[0], MatchEvaluator);
            partial = temp.Split('\0');
            // 对于第二部分，拆分并对参数进行加工
            // 第二部分的长度与 args 的长度相等
            string machining, pattern;
            string[] path;
            object parent;
            for (int i = 1; i <= args.Length; i++)
            {
                // 哦槽好麻烦……这里将直接作暴力的替换。
                // TODO: 在保证效率的前提下，将此过程替换。
                machining = formats[i];
                if (machining == "") continue;
                pattern = args[i - 1].ToString();
                machining = Reg.Replace(machining, pattern);
                path = machining.Split('|');
                parent = FileManager.Create(path[0]);
                args[i] = PathHelper.LoadChild(parent, path);
            }
            // 设置参数顺序
            int count = 0;
            // 对于剩下的部分，依次根据参数获取之
            for (int i = args.Length + 1; i < formats.Length; i++)
                if (formats[i] == "||")
                {
                    count++;
                    if (count >= args.Length)
                        break;
                }
                else
                {
                    object obj = PathHelper.LoadChild(args[count], formats[i]);
                    if (obj is FuzzyFixnum)
                        objects.Add(new LongFactor(FormatTemp[i - args.Length - 1], obj as FuzzyFixnum));
                    else if (obj is FuzzyString)
                        objects.Add(new StringFactor(FormatTemp[i - args.Length - 1], obj as FuzzyString));
                    else
                        objects.Add(new ObjectFactor(obj));
                }
            // 依次挂起事件信息
            foreach (Factor factor in objects)
            {
                if (factor is LongFactor)
                    (factor as LongFactor).ValueChanged += new EventHandler<FuzzyValueChangedEventArgs<long>>(ValueChanged);
                else if(factor is StringFactor)
                    (factor as StringFactor).ValueChanged += new EventHandler<FuzzyValueChangedEventArgs<string>>(ValueChanged);
            }
        }

        void ValueChanged(object sender, FuzzyValueChangedEventArgs<string> e)
        {
            OnTextChanged();
        }
        void ValueChanged(object sender, FuzzyValueChangedEventArgs<long> e)
        {
            OnTextChanged();
        }
        void OnTextChanged()
        {
            if (TextChanged != null)
                TextChanged(this, new EventArgs());
        }

        protected List<string> FormatTemp = new List<string>();
        protected string MatchEvaluator(Match match)
        {
            CaptureCollection collection = match.Groups[2].Captures;
            FormatTemp.Add(collection.Count == 0 ? "" : "{" + collection[0].Value + "}");
            return KeyChar;
        }

        public string GetString()
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            for (; i < objects.Count; i++)
            {
                sb.Append(partial[i]);
                sb.Append(objects[i].GetString());
            }
            for (; i < partial.Length; i++)
                sb.Append(partial[i]);
            return sb.ToString();
        }

        public override string ToString()
        {
            return GetString();
        }


        abstract class Factor
        {
            abstract public string GetString();
        }
        abstract class EventedFactor<T> : Factor
        {
            abstract public event EventHandler<FuzzyValueChangedEventArgs<T>> ValueChanged;
            abstract protected void OnValueChanged(object sender, FuzzyValueChangedEventArgs<T> e);
        }
        class StringFactor : EventedFactor<string>
        {
            FuzzyString value;
            string Format;
            public override event EventHandler<FuzzyValueChangedEventArgs<string>> ValueChanged;
            public override string GetString()
            {
                return string.Format(Format, value.Text);
            }
            protected override void OnValueChanged(object sender, FuzzyValueChangedEventArgs<string> e)
            {
                if (ValueChanged != null)
                    ValueChanged(this, e);
            }
            public StringFactor(string Format, FuzzyString value)
            {
                this.Format = Format;
                this.value = value;
                value.ValueChanged += OnValueChanged;
            }
            public StringFactor(FuzzyString value)
            {
                Format = "{0}";
                this.value = value;
                value.ValueChanged += OnValueChanged;
            }
        }
        class LongFactor : EventedFactor<long>
        {
            FuzzyFixnum value;
            string Format;
            public override event EventHandler<FuzzyValueChangedEventArgs<long>> ValueChanged;
            public override string GetString()
            {
                return string.Format(Format, value.Value);
            }
            protected override void OnValueChanged(object sender, FuzzyValueChangedEventArgs<long> e)
            {
                if (ValueChanged != null)
                    ValueChanged(this, e);
            }
            public LongFactor(string Format,FuzzyFixnum value)
            {
                this.Format = Format;
                this.value = value;
                value.ValueChanged += OnValueChanged;
            }
        }
        class ObjectFactor : Factor
        {
            object value;
            public ObjectFactor(object value)
            {
                this.value = value;
            }
            public override string GetString()
            {
                return value.ToString();
            }
        }

    }
}
