using System;
using System.Collections.Generic;
using System.Text;
using DataEditor.FuzzyData;

namespace DataEditor.Control.ProtoType.Any
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
    public partial class Any
    {
        static public Type Bool = typeof(FuzzyBool);
        static public Type Color = typeof(FuzzyColor);
        static public Type Fixnum = typeof(FuzzyFixnum);
        static public Type Float = typeof(FuzzyFloat);
        static public Type Nil = typeof(FuzzyNil);
        static public Type Rect = typeof(FuzzyRect);
        static public Type Regexp = typeof(FuzzyRegexp);
        static public Type String = typeof(FuzzyString);
        static public Type Symbol = typeof(FuzzySymbol);
        static public List<Type> SupportedSingle = new List<Type>() 
          { Bool, Color, Fixnum, Float, Nil, Rect, Regexp, String, Symbol };
        static public Dictionary<Type, int> SuppotedIndex = new Dictionary<Type, int>()
        {
            {Bool,0},{Color,1},{Fixnum,2},{Float,3},{Nil,4},
            {Rect,5},{Regexp,6},{String,7},{Symbol,8}
        };
        static public Dictionary<Type, string> SupportedName = new Dictionary<Type, string>()
        {
            {Bool,"Bool"},{Color,"Color"},{Fixnum,"Fixnum"},{Float,"Float"},{Nil,"Nil"},
            {Rect,"Rect"},{Regexp,"Regexp"},{String,"String"},{Symbol,"Symbol"}
        };
        static public object newValue(int index)
        {
            switch (index)
            {
                case 0: return FuzzyBool.False;
                case 1: return new FuzzyColor(System.Drawing.Color.Transparent);
                case 2: return new FuzzyFixnum(0);
                case 3: return new FuzzyFloat(0.0);
                case 4: return FuzzyNil.Instance;
                case 5: return new FuzzyRect(0, 0, 0, 0);
                case 6: return new FuzzyRegexp(new FuzzyString(""), FuzzyRegexpOptions.None);
                case 7: return new FuzzyString("");
                case 8: return FuzzySymbol.GetSymbol("");
                default: return null;
            }
        }
    }

    public partial class Any
    {
        static public Type Array = typeof(FuzzyArray);
        static public Type Hash = typeof(FuzzyHash);
        static public Type Object = typeof(FuzzyObject);
        static public List<Type> SupportedComplex = new List<Type>() 
        { Array, Hash, Object };
    }
}
