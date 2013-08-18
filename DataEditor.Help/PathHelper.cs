using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using DataEditor.FuzzyData;

namespace DataEditor.Help
{
    public static class PathHelper
    {
        public static DirectoryInfo GetDirectory(string Name)
        {
            if (!Directory.Exists(Name))
                Directory.CreateDirectory(Name);
            return new DirectoryInfo(Name);
        }
        public static string CheckName(string fullname, bool ex = false)
        {
            if (ex)
            {
                string directory = System.IO.Path.GetDirectoryName(fullname);
                if (directory == "")
                    directory = Directory.GetCurrentDirectory();
                string File = Path.GetFileName(fullname);
                DirectoryInfo DirectoryInfo = new DirectoryInfo(directory);
                if (!DirectoryInfo.Exists)
                    return "";
                FileInfo[] files = DirectoryInfo.GetFiles(File + "*");
                if (files.Length == 0)
                    return "";
                return files[0].FullName;
            }
            else
            {
                FileInfo GottenFileInfo = new FileInfo(fullname);
                return GottenFileInfo.Exists ? GottenFileInfo.FullName : "";
            }
        }
        public static object LoadFile(string expression)
        {
            string[] names = expression.Split(new string[] { "|" },
               StringSplitOptions.RemoveEmptyEntries);
            object root = SerializationManager.LoadFile(Help.NounConverter.Get(names[0]));
            return LoadRoot(names, root);
        }
        /// <summary>
        /// 依次载入此数据的子数据节点。
        /// 注意：将不会载入或处理 path 的第一部分。
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static object LoadChild(object parent, string path)
        {
            string[] names = path.Split(new string[] { "|" },
              StringSplitOptions.None);
            return LoadRoot(names, parent);
        }
        public static object LoadChild(object parent, string[] path)
        {
            return LoadRoot(path, parent);
        }
        private static object LoadNext(string name, object node)
        {
            int num = -1;
            if (int.TryParse(name, out num))
            {
                if (node is Array)
                    if (num > 0 && num < (node as Array).Length)
                        return (node as Array).GetValue(num);
                if (node is FuzzyArray)
                    if (num > 0 && num < (node as FuzzyArray).Count)
                        return (node as FuzzyArray)[num];
            }
            FuzzyObject obj = node as FuzzyObject;
            if (obj == null)
                throw new ArgumentException(" Giving a wrong object when searching " + name);
            if (!name.StartsWith("@"))
                name = "@" + name;
            FuzzySymbol sym = FuzzySymbol.GetSymbol(name);
            if (obj.InstanceVariables.ContainsKey(sym))
                return obj.InstanceVariables[sym];
            throw new ArgumentException(" Not defined : " + name + " in " + node.ToString());

        }
        private static object LoadRoot(string[] path, object root)
        {
            object node = root;
            for (int i = 1; i < path.Length; i++)
                node = LoadNext(path[i], node);
            return node;
        }




        /// <summary>
        /// 根据数组，获取其子节点信息。
        /// </summary>
        /// <param name="Root">根数据</param>
        /// <param name="Path">一个由路径组成的数组</param>
        /// <returns>子数据信息</returns>
        static public object LoadByArray(object Root, object[] Path)
        {
            object temp = Root;
            foreach ( object key in Path )
            {
                if ( temp == null ) return null;
                if ( temp is FuzzyData.FuzzyArray )
                    if ( key is int )
                        temp = LoadArray(temp as FuzzyData.FuzzyArray, Convert.ToInt32(temp));
                    else if ( key is FuzzyData.FuzzyFixnum )
                        temp = LoadArray(temp as FuzzyData.FuzzyArray, key as FuzzyData.FuzzyFixnum);
                    else if ( key is string )
                        temp = LoadArray(temp as FuzzyData.FuzzyArray, key as string);
                    else
                        throw new ArgumentException("无法匹配的参数： [" + temp.GetType().ToString() + "] On " + temp.ToString());
                else if ( temp is FuzzyData.FuzzyObject )
                    if ( key is String )
                        temp = LoadChild(temp as FuzzyData.FuzzyObject, key as String);
                    else if ( key is FuzzyData.FuzzyString )
                        temp = LoadChild(temp as FuzzyData.FuzzyObject, key as FuzzyData.FuzzyString);
                    else
                        throw new ArgumentException("无法匹配的参数： [" + temp.GetType().ToString() + "] On " + temp.ToString());

            }
            return temp;
        }
        static object LoadChild(FuzzyData.FuzzyObject Object, string Key)
        {
            if (!(Key.StartsWith("@"))) Key = "@" + Key;
            FuzzyData.FuzzySymbol sym = FuzzyData.FuzzySymbol.GetSymbol(Key);
            if (Object.InstanceVariables.ContainsKey(sym)) return Object.InstanceVariables[sym];
            else return null;
        }
        static object LoadChild(FuzzyData.FuzzyObject Object, FuzzyData.FuzzyString Key) { return LoadChild(Object, Key.Text); }
        static object LoadArray(FuzzyData.FuzzyArray Object, int Key)
        {
            if (Key < 0 || Key >= Object.Length) return null;
            return Object[Key];
        }
        static object LoadArray(FuzzyData.FuzzyArray Object, string Key)
        {
            int i = 0;
            if (int.TryParse(Key, out i)) return LoadArray(Object, i);
            return null;
        }
        static object LoadArray(FuzzyData.FuzzyArray Object, FuzzyData.FuzzyFixnum Key) { return LoadArray(Object, Convert.ToInt32(Key.Value)); }
    }
}
