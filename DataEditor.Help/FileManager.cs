using System;
using System.Collections.Generic;
using System.Text;
using DataEditor.FuzzyData;

namespace DataEditor.Help
{
    public partial class FileManager
    {
        protected object data;
        static protected Dictionary<string, FileManager> files = new Dictionary<string, FileManager>();
        protected FileManager(string expression)
        {
            data = PathHelper.LoadFile(expression);
        }
        static public FileManager Create(string path)
        {
            if (!files.ContainsKey(path))
                files.Add(path, new FileManager(path));
            return files[path];
        }
    }

    public partial class FileManager
    {
        public event EventHandler<FileChangeEventArgs> FileChanged;
        public void CallChange(FileChangeEventArgs e)
        {
            if (FileChanged != null)
                FileChanged(this, e);
        }
    }

    public class FileArrayManager : FileManager
    {
        protected new FuzzyArray data;
        List<FuzzyObject> value = new List<FuzzyObject>();
        LinkTable<int, int> link = new LinkTable<int, int>();
        protected FileArrayManager(string expression)
            : base(expression)
        {
            link.Clear();
            value.Clear();
            if (!(base.data is FuzzyArray))
                return;
            data = base.data as FuzzyArray;
            int i = -1, j = 0;
            foreach (object o in data)
            {
                i++;
                if (o != null && o is FuzzyObject && o != FuzzyNil.Instance)
                {
                    value.Add(o as FuzzyObject);
                    link.Add(i, j++);
                }
            }
        }
        public FuzzyArray Data { get { return data; } }
        public List<FuzzyObject> Value { get { return value; } }
        public LinkTable<int, int> Link { get { return link; } }
        public FuzzyObject this[int index] // index is j
        {
            get { return value[link.Reverse[index]]; }
            set { this.value[link.Reverse[index]] = value; }
        }
        public new static FileArrayManager Create(string path)
        {
            if (files.ContainsKey(path))
                if (files[path] is FileArrayManager)
                    return files[path] as FileArrayManager;
                else 
                    files[path] = new FileArrayManager(path);
            else 
                files.Add(path,new FileArrayManager(path));
            return files[path] as FileArrayManager;
        }
    }

    public class FileChangeEventArgs : EventArgs
    {
        internal List<int> changeTable;
        internal int deviation;

        public FileChangeEventArgs() : this(FileChangeType.None, 0) { }
        public FileChangeEventArgs(FileChangeType type, int num)
        {
            changeTable = new List<int>();
            switch (type)
            {
                case FileChangeType.None:
                    deviation = 0;
                    break;
                case FileChangeType.Add:
                    for (int i = 0; i < num; i++)
                        changeTable.Add(i);
                    deviation = 1;
                    break;
                case FileChangeType.Delete:
                    for (int i = 0; i < num; i++)
                        changeTable.Add(i);
                    changeTable.Add(-1);
                    deviation = -1;
                    break;
            }
        }
        public FileChangeEventArgs(List<int> changeTable,int deviation)
        {
            this.changeTable = changeTable;
            this.deviation = deviation;
        }

        public int this[int i]
        {
            get 
            {
                if (i == -1)
                    return -1;
                if (changeTable.Count <= i)
                    return i + deviation;
                return changeTable[i];
            }
        }

        public static FileChangeEventArgs Swap(int i1, int i2)
        {
            int min = i1 < i2 ? i1 : i2;
            int max = i1 > i2 ? i1 : i2;
            List<int> changeTable = new List<int>();
            for (int i = 0; i < min; i++)
                changeTable.Add(i);
            changeTable.Add(max);
            for (int i = min + 1; i < max; i++)
                changeTable.Add(i);
            changeTable.Add(min);
            return new FileChangeEventArgs(changeTable, 0);
        }
        public static FileChangeEventArgs Move(int from, int to)
        {
            if (to < from)
                return MoveUp(from, to);
            return MoveDown(from, to);
        }
        public static FileChangeEventArgs Add(int num)
        {
            return new FileChangeEventArgs(FileChangeType.Add, num);
        }
        public static FileChangeEventArgs Delete(int num)
        {
            return new FileChangeEventArgs(FileChangeType.Delete, num);
        }
        public static FileChangeEventArgs operator +(FileChangeEventArgs f1, FileChangeEventArgs f2)
        {
            int deviation = f1.deviation + f2.deviation;
            int f1size = f1.changeTable.Count + (f1.deviation >= 0 ? f1.deviation : 0);
            int f2size = f2.changeTable.Count + (f2.deviation >= 0 ? f2.deviation : 0);
            int size = f1size > f2size ? f1size : f2size;
            List<int> changeTable = new List<int>();
            for (int i = 0; i < size; i++)
                changeTable.Add(0);
            for (int i = 0; i < size; i++)
                changeTable[i] = f1[f2[i]];
            return new FileChangeEventArgs(changeTable, deviation);
        }
        protected static FileChangeEventArgs MoveUp(int from, int to)
        {
            List<int> changeTable = new List<int>();
            for (int i = 0; i < to; i++)
                changeTable.Add(i);
            changeTable.Add(from);
            for (int i = to; i < from; i++)
                changeTable.Add(i);
            return new FileChangeEventArgs(changeTable, 0);
        }
        protected static FileChangeEventArgs MoveDown(int from, int to)
        {
            List<int> changeTable = new List<int>();
            for (int i = 0; i < from; i++)
                changeTable.Add(i);
            for (int i = from; i < to; i++)
                changeTable.Add(i + 1);
            changeTable.Add(from);
            return new FileChangeEventArgs(changeTable, 0);
        }
    }
    public enum FileChangeType 
    {
        None,
        Add,
        Delete
    }
}
