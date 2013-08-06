using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace DataEditor.Help
{
    public static class InitalizedObjectEditorHelper
    {
        static System.IO.DirectoryInfo Dir;
        static System.IO.FileInfo File;
        static InitalizedObjectEditorHelper()
        {
            Dir = Help.PathHelper.GetDirectory("Program/Options/Controls");
        }
        static string GetFullName(String Name)
        {
            System.IO.FileInfo[] FileInfos = Dir.GetFiles(Name + ".option");
            if (FileInfos.Length == 0)
                return "";
            File = FileInfos[0];
            return File.FullName;
        }
        public static object GetOption(object ob)
        {
            return GetOption(ob.GetType());
        }
        public static object GetOption(Type type)
        {
            string Name1 = type.Name;
            string Name2 = type.FullName;
            string str = GetFullName(Name1);
            if (str != "")
                return GetOption(str);
            str = GetFullName(Name2);
            if (str != "")
                return GetOption(str);
            return null;
        }
        static object GetOption(string FullName)
        {
            FileStream stream;
            try
            {
                stream = new FileStream(FullName, FileMode.Open);
            }
            catch 
            {
                return null;
            }
            object arguments = ser.Deserialize(stream);
            stream.Close();
            return arguments;
        }
        static System.Runtime.Serialization.Formatters.Binary.BinaryFormatter ser = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        public static void SetOption(Type type, object arguments)
        {
            string Fullname = "Program/Options/Controls/" + type.FullName + ".option";
            FileStream stream = new FileStream(Fullname, FileMode.Create);
            ser.Serialize(stream, arguments);
            stream.Close();
        }
        public static void SetOption(object ob, object arguments)
        {
            SetOption(ob.GetType(), arguments);
        }
    }
}
