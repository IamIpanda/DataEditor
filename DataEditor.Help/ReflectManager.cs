﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

namespace DataEditor.Help
{
    public static class ReflectLoader
    {
        static Dictionary<FileInfo, Assembly> Assemblies = new Dictionary<FileInfo, Assembly>();
        public static Assembly GetAssembly(FileInfo info)
        {
            if (info.Exists)
            {
                if (Assemblies.ContainsKey(info))
                    return Assemblies[info];
                Assembly ass = Assembly.LoadFrom(info.FullName);
                Assemblies.Add(info, ass);
                return ass;
            }
            throw new ArgumentException("File Not Exists : " + info.FullName);
        }
        public static List<Assembly> GetDirectory(DirectoryInfo info)
        {
            if(!info.Exists)
                throw new ArgumentException("Directory Not Exists : " + info.FullName);
            FileInfo[] files = info.GetFiles("*.dll");
            List<Assembly> ass = new List<Assembly>();
            foreach (FileInfo file in files)
                ass.Add(GetAssembly(file));
            return ass;
        }
        public static Contract.Addin GetDescription(Assembly ass)
        {
            Type addin = typeof(Contract.Addin);
            foreach (Type type in ass.GetExportedTypes())
                foreach (Type Interface in type.GetInterfaces())
                    if (Interface == addin)
                        return ass.CreateInstance(type.FullName) as Contract.Addin;
            return null;
        }
    }
}
