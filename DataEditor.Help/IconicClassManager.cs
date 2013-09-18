using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;

namespace DataEditor.Help
{
    public abstract class IconicClassManager<T> where T : class,Contract.Iconic
    {
        abstract protected string Path { get; }
        abstract protected string GetLog (T obj);
        abstract protected Dictionary<string, Type> Types { get; }
        public static IconicClassManager<T> Instance { get; set; }
        protected IconicClassManager () { }

        static public void Initialize()
        {
            DirectoryInfo directory = new DirectoryInfo(Instance.Path);
            List<Assembly> assemblies = ReflectLoader.GetDirectory(directory);

            foreach ( Assembly ass in assemblies )
                Instance.AddAssembly(ass);
        }
        void AddAssembly (Assembly ass)
        {
            Log.log("正在导入库：" + ass.FullName);
            Type BaseType = typeof(T);
            foreach ( Type type in ass.GetExportedTypes() )
            {
                try
                {
                    if ( type.IsClass && !type.IsAbstract )
                        foreach ( Type Inter in type.GetInterfaces() )
                            if ( Inter == BaseType )
                            {
                                object obj = ass.CreateInstance(type.FullName);
                                T instance = obj as T;
                                string key = instance.Flag;
                                if ( !Types.ContainsKey(key) )
                                { Types.Add(key, type); Log.log(GetLog(instance)); }
                            }
                }
                catch ( Exception ex )
                { Log.log("导入类型" + type.FullName + "时发生了错误：" + ex.ToString()); }
            }
        }

        public enum SearchMode { Full, Start, Conclude };
        public Type Get (string key, SearchMode Mode = SearchMode.Full)
        {
            if ( Mode == SearchMode.Full )
            {
                Type ans = typeof(object);
                Types.TryGetValue(key, out ans);
                return ans;
            }
            else if ( Mode == SearchMode.Start )
            {
                foreach ( string prefix in Types.Keys )
                    if ( key.StartsWith(prefix) ) return Types[key];
                return typeof(object);
            }
            else if ( Mode == SearchMode.Conclude )
            {
                foreach ( string prefix in Types.Keys )
                    if ( key.Contains(prefix) ) return Types[key];
                return typeof(object);
            }
            return typeof(int);
        }
        public T GetInstance (string key, SearchMode Mode = SearchMode.Full)
        {
            Type type = Get(key, Mode);
            if ( type == typeof(object) ) return null;
            try { return Assembly.GetAssembly(type).CreateInstance(type.FullName) as T; }
            catch { return null; }
        }
    }
}
