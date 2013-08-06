using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Help
{
    public class SimpleCollection<T>
    {
        List<T> Value;
        public List<T> ActualValue { get { return Value; } }
        public SimpleCollection(List<T> value)
        {
            this.Value = value;
        }
        public SimpleCollection()
        {
            this.Value = new List<T>();
        }
        public T this[int i]
        {
            get
            {
                if (i >= 0 && i < Value.Count)
                    return Value[i];
                else
                    return default(T);
            }
            set
            {
                if (i > 0 && i < Value.Count)
                    Value[i] = value;
            }
        }
    }

    public class HashCollection<T1, T2>
    {
        Dictionary<T1, T2> Value;
        public Dictionary<T1, T2> ActualValue { get { return Value; } }
        public HashCollection(Dictionary<T1, T2> Value)
        {
            this.Value = Value;
        }
        public HashCollection()
        {
            this.Value = new Dictionary<T1, T2>();
        }
        public T2 this[T1 Key]
        {
            get 
            {
                if (Value.ContainsKey(Key))
                    return Value[Key];
                else
                    return default(T2);
            }
            set 
            {
                if (Value.ContainsKey(Key))
                    Value[Key] = value;
                else
                    Value.Add(Key, value);
            }
        }
    }
}
