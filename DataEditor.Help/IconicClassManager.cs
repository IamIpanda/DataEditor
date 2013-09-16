using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;

namespace DataEditor.Help
{
    public abstract class IconicClassManager<T> where T : Contract.Iconic where T : class 
    {
        abstract static string Path { get; }
        abstract static string GetLog (T obj);
        abstract static Dictionary<string, Type> Types { get; }

        static IconicClassManager ()
        {
            DirectoryInfo directory = new DirectoryInfo(Path);
            List<Assembly> assemblies = ReflectLoader.GetDirectory(directory);

            foreach ( Assembly ass in assemblies )
                AddAssembly(ass);
        }
        static void AddAssembly (Assembly ass)
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
        public static Type Get (string key, SearchMode Mode = SearchMode.Full)
        {
            return typeof(int);
        }
        public static T GetInstance (string key, SearchMode Mode = SearchMode.Full)
        {
            return null;
        }
    }
}
