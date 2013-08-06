using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace DataEditor.Help
{
    [Serializable]
    public struct Rtp
    {
            public string Path, Version, Name;
            public System.Drawing.Color Color;
            public bool IsFromReg;
            public Rtp(string Path, string Version, String Name, System.Drawing.Color Color,bool IsFromReg)
            {
                this.Path = Path;
                this.Version = Version;
                this.Name = Name;
                this.Color = Color;
                this.IsFromReg = IsFromReg;
            }
            public override string ToString()
            {
                return Name == "" ? Path : (Name + (Version == "" ? "" : ("（" + Version + "）")));
            }
            public string FullString()
            {
                return "[" + Name + "(" + Version + ")" + "]" + Path;
            }
            
            static public Rtp FromString(String s, Color c)
            {
                Regex r = new Regex("\\[.+\\]");
                Regex r2 = new Regex("\\(.+\\)");
                Match m = r.Match(s);
                Match m2;
                String path, version, name;
                if (m.Success)
                {
                    path = s.Remove(m.Index, m.Length);
                    string s2 = m.Value;
                    s2 = s2.Remove(0, 1);
                    s2 = s2.Remove(s2.Length - 1, 1);
                    m2 = r2.Match(s2);
                    if (m2.Success)
                    {
                        name = s2.Remove(m2.Index, m2.Length);
                        version = m2.Value;
                        version = version.Remove(0, 1);
                        version = version.Remove(version.Length - 1, 1);
                    }
                    else
                    {
                        name = s2;
                        version = "";
                    }
                }
                else
                {
                    version = "";
                    name = "";
                    path = s;
                }
                return new Rtp(path, version, name, c, false);
            }
    }
    static public class RtpManager
    {
        static readonly private List<Color> colors = new List<Color>() {
            Color.DarkRed, 
            Color.Orange,
            Color.YellowGreen,
            Color.BlueViolet,
            Color.Purple 
        };
        static List<Rtp> RtpList = new List<Rtp>();
        static public List<Rtp> Rtp { get { return RtpList; } set { RtpList = value; Save(); } }
        static RtpManager()
        {
            RtpList.Add(new Rtp(System.IO.Directory.GetCurrentDirectory(), "", "Self Directory", Color.Gray, true));
            GetRegistryRtps();
            Load();
        }
        static void GetRegistryRtps()
        {
            RegistryKey l = Registry.LocalMachine;
            RegistryKey s = l.OpenSubKey("SOFTWARE");
            RegistryKey E = s.OpenSubKey("Enterbrain");
            int count = 0;
            foreach (string t in E.GetSubKeyNames())
                if (t == "RGSS" || t == "RGSS2" || t == "RGSS3")
                {
                    RegistryKey d = E.OpenSubKey(t).OpenSubKey("RTP");
                    foreach (string r in d.GetValueNames())
                    {
                        RtpList.Add(new Rtp(d.GetValue(r).ToString(), t, r, GetColor(count), true));
                        count++;
                    }
                }
        }
        static Color GetColor(int count)
        {
            if (count < colors.Count)
                return colors[count];
            int index = count % colors.Count;
            int pow = count / colors.Count;
            int alpha = (int)(255 / Math.Pow(2.0, pow));
            return Color.FromArgb(alpha, colors[index]);
        }
        static public string SearchFile(string name)
        {
            foreach (Rtp rtp in RtpList)
            {
                string partialName = Path.Combine(rtp.Path, name);
                string fullname = PathHelper.CheckName(partialName, true);
                if (fullname != null && fullname != "")
                    return fullname;
            }
            return "";
        }
        static System.Runtime.Serialization.Formatters.Binary.BinaryFormatter ser
            = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        static public void Save()
        {
            List<Rtp> saveRtps = new List<Rtp>();
            foreach (Rtp rtp in RtpList)
                if (rtp.IsFromReg == false)
                    saveRtps.Add(rtp);
            FileStream stream = new FileStream("Program/Options/Rtp.option", FileMode.Create);
            ser.Serialize(stream, saveRtps);
            stream.Close();
        }
        static public void Load()
        {
            FileInfo info = new FileInfo("Program/Options/Rtp.option");
            if (!info.Exists)
                return;
            FileStream stream = new FileStream(info.FullName, FileMode.Open);
            List<Rtp> rtps = ser.Deserialize(stream) as List<Rtp>;
            if (rtps != null)
                RtpList.AddRange(rtps);
        }
    }
}
