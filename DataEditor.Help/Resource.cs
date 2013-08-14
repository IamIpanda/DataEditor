using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DataEditor.Help
{
    public static class Resource
    {
        static List<string> resource = new List<string>();
        static public void Load ()
        {
            string FullName = "Program/strings.conf";
            if (!(System.IO.File.Exists(FullName))) return;
            StreamReader reader = new StreamReader(new FileStream(FullName, FileMode.OpenOrCreate));
            string s;
            while ( !(reader.EndOfStream) )
                if ( !((s = reader.ReadLine()).StartsWith("#")) )
                    resource.Add(s);
        }
        static Resource()
        {
            Load();
            r = new ResourceGetter();
        }
        static ResourceGetter r;
        static public ResourceGetter Get { get { return r; } }
        public class ResourceGetter
        {
            public string this[int x]
            {
                get { if ( x < 0 || x >= resource.Count ) return ""; else return resource[x]; }
            }
        }
    }
}
