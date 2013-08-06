﻿using System;
using System.Collections.Generic;
using System.Text;
using DataEditor.Help;
using System.IO;
using System.Reflection;

namespace DataEditor.Arce.Interpreter
{
    public class Collector
    {
        static Dictionary<string, Type> Types = new Dictionary<string, Type>();
        static public Collector Instance { get; set; }
        static Collector()
        {
            DirectoryInfo directory = new DirectoryInfo("Program/Control/Wrapper");
            if (!directory.Exists) return;
            List<Assembly> la = ReflectLoader.GetDirectory(directory);

            foreach (Assembly ass in la)
                AddAssembly(ass);
            Instance = new Collector();
        }
        static public void AddAssembly(Assembly ass)
        {
            Type basetype = typeof(DataEditor.Control.ObjectEditor);
            foreach (Type t in ass.GetExportedTypes())
                if (t.IsClass)
                    foreach (Type Inter in t.GetInterfaces())
                        if (Inter == basetype)
                        {
                            object o = ass.CreateInstance(t.FullName);
                            string key = (o as DataEditor.Control.ObjectEditor).Flag;
                            if (!Types.ContainsKey(key))
                                Types.Add(key, t);
                        }
        }
        protected Collector() { }
        public object this[string s]
        {
            get
            {
                if (Types.ContainsKey(s))
                {
                    Type type = Types[s];
                    Assembly ass = Assembly.GetAssembly(type);
                    object target = ass.CreateInstance(type.FullName);
                    return target;
                }
                else
                   return null;
            }
        }

    }
}
