using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Help
{
    public class LinkTable<T1,T2>
    {
        Dictionary<T1, T2> Plus = new Dictionary<T1, T2>();
        Dictionary<T2, T1> Minus = new Dictionary<T2, T1>();
        LinkInterface<T1, T2> verse;
        LinkInterface<T2, T1> reverse;
        public LinkTable()
        {
            verse = new LinkInterface<T1, T2>(Plus);
            reverse = new LinkInterface<T2, T1>(Minus);
        }
        public void Add(T1 key,T2 value)
        {
            Plus.Add(key, value);
            Minus.Add(value, key);
        }
        public void Clear()
        {
            Plus.Clear();
            Minus.Clear();
        }
        public T2 this[T1 Key] { get { return verse[Key]; } }
        public T1 this[T2 Key] { get { return reverse[Key]; } }
        public LinkInterface<T1, T2> Verse { get { return verse; } }
        public LinkInterface<T2, T1> Reverse { get { return reverse; } }
        public class LinkInterface<T3, T4>
        {
            Dictionary<T3, T4> dictionary;
            internal LinkInterface(Dictionary<T3,T4> dictionary)
            {
                this.dictionary = dictionary;
            }
            public T4 this[T3 argument]
            {
                get 
                {
                    T4 value = default(T4);
                    dictionary.TryGetValue(argument, out value);
                    return value;
                }
            }
        }
    }
}
