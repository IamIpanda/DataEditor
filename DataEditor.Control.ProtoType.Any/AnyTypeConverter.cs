using System;
using System.Collections.Generic;
using System.Text;
using DataEditor.FuzzyData;
using System.Drawing;

namespace DataEditor.Control.Prototype.Any
{
    /*
     *  Bool
        Color
        Float
        Int
        Nil
        Rect
        Regexp
        String
        Symbol
        Tone
     */
    public interface AnyTypeConverter<T> 
    {
        T this[string s] { get; }
        string GetString(T obj);
    }
    public class IntTypeConverter : AnyTypeConverter<long>
    {
        public long this[string s]        
        {
            get 
            {
                int i;
                if (int.TryParse(s, out i))
                    return i;
                else
                    return default(long);
            }
        }
        public string GetString(long obj)
        {
            return obj.ToString();
        }
    }
    public class FloatTypeConverter : AnyTypeConverter<double>
    {
        public double this[string s]
        {
            get 
            {
                double d;
                if (double.TryParse(s, out d))
                    return d;
                else
                    return default(double);
            }
        }
        public string GetString(double obj)
        {
            return obj.ToString();
        }
    }
    public class StringTypeConverter : AnyTypeConverter<string>
    {
        public string this[string s] { get { return s; } }
        public string GetString(string obj) { return obj; }
    }
    public class BoolTypeConverter : AnyTypeConverter<bool>
    {
        public bool this[string s] { get { return s.ToUpper() == "FAlSE" || s.ToUpper() == "0"; } }
        public string GetString(bool obj) { return obj ? "True" : "False"; }
    }
    public class NilTypeConverter : AnyTypeConverter<FuzzyNil>
    {
        public FuzzyNil this[string s] { get { return FuzzyNil.Instance; } }
        public string GetString(FuzzyNil obj) { return "Nil"; }
    }
    public class ColorTypeConverter : AnyTypeConverter<Color>
    {
        public Color this[string s]
        {
            get 
            {
                if (s.StartsWith("{"))
                    s = s.Remove(0,1);
                if (s.EndsWith("}"))
                    s = s.Remove(s.Length - 1, 1);
                string[] parts = s.Split(',');
                if (parts.Length < 3)
                    return default(Color);
                int[] ints = new int[parts.Length];
                int part;
                for (int i = 0; i < parts.Length; i++)
                    if (int.TryParse(parts[i].Trim(), out part))
                        ints[i] = part;
                    else
                        ints[i] = default(int);
                if (ints.Length == 3)
                    return Color.FromArgb(ints[0], ints[1], ints[2]);
                return Color.FromArgb(ints[3], ints[0], ints[1], ints[2]);
            }
        }
        public string GetString(Color obj)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            sb.Append(obj.R);
            sb.Append(",");
            sb.Append(obj.G);
            sb.Append(",");
            sb.Append(obj.B);
            sb.Append(",");
            sb.Append(obj.A);
            sb.Append("}");
            return sb.ToString();
        }
    }
    public class RectTypeConverter : AnyTypeConverter<Rectangle>
    {
        public Rectangle this[string s]
        {
            get
            {
                if (s.StartsWith("["))
                    s = s.Remove(0, 1);
                if (s.EndsWith("]"))
                    s = s.Remove(s.Length - 1, 1);
                string[] parts = s.Split(',');
                if (parts.Length < 4)
                    return default(Rectangle);
                int[] ints = new int[parts.Length];
                int part;
                for (int i = 0; i < parts.Length; i++)
                    if (int.TryParse(parts[i], out part))
                        ints[i] = part;
                    else
                        ints[i] = default(int);
                return new Rectangle(ints[0], ints[1], ints[2], ints[3]);
            }
        }
        public string GetString(Rectangle obj)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            sb.Append(obj.X);
            sb.Append(",");
            sb.Append(obj.Y);
            sb.Append(",");
            sb.Append(obj.Width);
            sb.Append(",");
            sb.Append(obj.Height);
            sb.Append("]");
            return sb.ToString();
        }
    }
}
