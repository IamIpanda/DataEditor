using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using DataEditor.Contract;

namespace DataEditor.Help
{
    static public class SerializationManager
    {
        static Serialization Default;
        static List<Serialization> serializations = new List<Serialization>();
        static Dictionary<string, Serialization> flags = new Dictionary<string, Serialization>();

        static SerializationManager()
        {
            LoadDlls();
        }
        static void LoadDlls()
        {
            DirectoryInfo Directory = PathHelper.GetDirectory("Program/Serialization");
            foreach (Assembly ass in ReflectLoader.GetDirectory(Directory))
                LoadDll(ass);
        }
        static void LoadDll(Assembly Ass)
        {
            foreach (Type serialization in Ass.GetExportedTypes())
                if (Fit(serialization))
                    serializations.Add(Ass.CreateInstance(serialization.ToString()) as Serialization);
            foreach (Serialization serialization in serializations)
                if (serialization is Iconic)
                    flags.Add((serialization as Iconic).Flag, serialization);
            foreach (Serialization serialization in serializations)
                if (serialization.GetType().Name == "RubyMarshalAdapter" || serialization.GetType().Name == "RubyMarshal")
                    Default = serialization;
            if (Default == null)
                Default = serializations[0];
        }
        static bool Fit(Type serialization)
        {
            Type ser = typeof(Contract.Serialization);
            foreach (Type t in serialization.GetInterfaces())
                if (t == ser)
                    return true;
            return false;
        }

        static public object LoadFile(string name)
        {
            // 选择一个合适的序列化器
            Serialization serialization = Default;
            foreach (string flag in flags.Keys)
                if (flag.StartsWith(flag))
                {
                    serialization = flags[flag];
                    name.Remove(0, flag.Length);
                    break;
                }
            // 搜查文件
            string fullname = RtpManager.SearchFile(name);
            if (fullname == "")
                throw new ArgumentException("Can't get the file : " + name);
            // 建立文件流
            FileStream stream;
            try
            {
                stream = new System.IO.FileStream(fullname, FileMode.Open);
            }
            catch(Exception ex)
            {
                throw new ArgumentException("An Error Occuued when Loading File " + fullname, ex);
            }
            // 进行序列化
            try
            {
                object ob = serialization.Load(stream);
                return ob;
            }
            catch(Exception ex)
            {
                throw new ArgumentException("An Error Occured when Serializing File" + fullname, ex);
            }
        }
    }
}
