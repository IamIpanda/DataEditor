using System;
using System.Collections.Generic;
using System.Text;
using DataEditor.FuzzyData;

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
    public interface AnyTypeBase
    {
        string Text { get; set; }
        object ObjectValue { get; set; }
    }
    public interface AnyTypeManager<T> : AnyTypeBase
    {
        T Value { get; set; }
    }
    public abstract class AnyTypeManagerHelp<T1, T2> : AnyTypeManager<T1>
        where T2 : new()
    {
        protected T2 tc = new T2();
        public T1 Value { get; set; }
        public object ObjectValue 
        {
            get { return Value; }
            set
            {
                if (value is T1)
                    Value = (T1)value;
            }
        }
        public abstract string Text { get; set; }
    }
    public class BoolTypeManager : AnyTypeManagerHelp<FuzzyBool, BoolTypeConverter>
    {
        public override string Text
        {
            get { return tc.GetString(Value.Value); }
            set { Value.Value = tc[value]; }
        }
    }
    public class ColorTypeManager : AnyTypeManagerHelp<FuzzyColor, ColorTypeConverter>
    {
        public override string Text
        {
            get { return tc.GetString(Value.Value); }
            set { Value.Value = tc[value]; }
        }
    }
    public class FloatTypeManager : AnyTypeManagerHelp<FuzzyFloat, FloatTypeConverter>
    {
        public override string Text
        {
            get { return tc.GetString(Value.Value); }
            set { Value.Value = tc[value]; }
        }
    }
    public class IntTypeManager : AnyTypeManagerHelp<FuzzyFixnum, IntTypeConverter>
    {
        public override string Text
        {
            get { return tc.GetString(Value.Value) ;}
            set { Value.Value = tc[value]; }
        }
    }
    public class NilTypeManager : AnyTypeManagerHelp<FuzzyNil, NilTypeConverter>
    {
        public override string Text
        {
            get { return tc.GetString(Value); }
            set { }
        }
    }
    public class RectTypeManager : AnyTypeManagerHelp<FuzzyRect, RectTypeConverter>
    {
        public override string Text
        {
            get { return tc.GetString(Value.Value); }
            set { Value.Value = tc[value]; }
        }
    }
    public class RegTypeManager : AnyTypeManagerHelp<FuzzyRegexp, StringTypeConverter>
    {
        public override string Text
        {
            get { return tc.GetString(Value.Pattern.Text); }
            set { Value.Pattern.Text = tc[value]; }
        }
    }
    public class StringTypeManager : AnyTypeManagerHelp<FuzzyString, StringTypeConverter>
    {
        public override string Text
        {
            get { return tc.GetString(Value.Text); }
            set { Value.Text = tc[value]; }
        }
    }
    public class SymbolTypeManager : AnyTypeManagerHelp<FuzzySymbol, StringTypeConverter>
    {
        public override string Text
        {
            get {
                return tc.GetString(Value.Name); }
            set {
                if (value.StartsWith(":"))
                    value = value.Remove(0, 1);
                Value = FuzzySymbol.GetSymbol(value); }
        }
    }
    public class ToneTypeManager : AnyTypeManagerHelp<FuzzyTone, ColorTypeConverter>
    {
        public override string Text
        {
            get { return tc.GetString(Value.Value); }
            set { Value.Value = tc[value]; }
        }
    }
    public class TypeManager
    {
        static Dictionary<Type, AnyTypeBase> TypeLink = new Dictionary<Type, AnyTypeBase>();
        static TypeManager()
        {
            TypeLink.Add(Any.Bool, new BoolTypeManager());
            TypeLink.Add(Any.Color, new ColorTypeManager());
            TypeLink.Add(Any.Fixnum, new IntTypeManager());
            TypeLink.Add(Any.Float, new FloatTypeManager());
            TypeLink.Add(Any.Nil, new NilTypeManager());
            TypeLink.Add(Any.Rect, new RectTypeManager());
            TypeLink.Add(Any.Regexp, new RegTypeManager());
            TypeLink.Add(Any.String, new StringTypeManager());
            TypeLink.Add(Any.Symbol, new SymbolTypeManager());
        }
        public object Value { get; set; }
        public string Text
        {
            get { return GetString(); }
            set { SetString(value); }
        }
        protected string GetString()
        {
            if (Value == null)
                return "";
            if (!TypeLink.ContainsKey(Value.GetType()))
                return "";
            AnyTypeBase ob = TypeLink[Value.GetType()];
            ob.ObjectValue = Value;
            return ob.Text;
        }
        protected object SetString(string value)
        {
            if (Value == null)
                return "";
            if (!TypeLink.ContainsKey(Value.GetType()))
                return "";
            AnyTypeBase ob = TypeLink[Value.GetType()];
            ob.ObjectValue = Value;
            ob.Text = value;
            return ob.ObjectValue;
        }
    }
}
