using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Help
{
    public class OriginCalculator
    {
        public enum NodeType
        {
            Num,
            Operator,
            Variable
        }
        public class CalcNode : ICloneable
        {
            public NodeType Type;
            public double Num;
            public char Value;
            public CalcNode(NodeType Type)
            {
                this.Type = Type;
                this.Num = 0.0;
                this.Value = '#';
            }
            public override string ToString()
            {
                if (Type == NodeType.Num)
                    return "CalcNode[Num]:" + Num.ToString();
                else if (Type == NodeType.Operator)
                    return "CalcNode[Ope]:" + Value.ToString();
                else if (Type == NodeType.Variable)
                    return "CalcNode[Var]:" + Value.ToString();
                else
                    return "CalcNode";
            }
            static public CalcNode FromChar(char c)
            {
                CalcNode cn = new CalcNode(NodeType.Operator);
                cn.Value = c;
                return cn;
            }
            static public CalcNode FromDouble(double d)
            {
                CalcNode cn = new CalcNode(NodeType.Num);
                cn.Num = d;
                return cn;
            }
            public object Clone()
            {
                return this.MemberwiseClone();
            }
        }
        public class MiddleExpression : List<CalcNode> { }
        public class PolishExpression : Stack<CalcNode> { }
        static int Rank(char c)
        {
            if (c == '+' || c == '-')
                return 1;
            else if (c == '*' || c == '/' || c == '%')
                return 2;
            else if (c == '^')
                return 3;
            else
                return 0;
        }
        static double Calc(double x, double y, char c)
        {
            if (c == '+')
                return x + y;
            else if (c == '-')
                return y - x;
            else if (c == '*')
                return x * y;
            else if (c == '/')
            {
                if (x == 0)
                    return 0;
                return y / x;
            }
            else if (c == '%')
                return x % y;
            else if (c == '^')
                return Math.Pow(y, x);
            else
                return 0;
        }
        static public MiddleExpression TurnToMiddle(string str)
        {
            string s = str.Replace(" ", "");
            MiddleExpression m = new MiddleExpression();
            CalcNode LastNode = new CalcNode(NodeType.Num);
            char[] cs = s.ToCharArray();
            LastNode = null;
            foreach (char c in cs)
            {
                if (c >= 48 && c <= 57)
                {
                    if (LastNode != null && LastNode.Type == NodeType.Num)
                        LastNode.Num = LastNode.Num * 10 + c - 48;
                    else
                    {
                        LastNode = CalcNode.FromDouble(c - 48.0);
                        m.Add(LastNode);
                    }
                }
                else if (c == '+' || c == '-' || c == '*' || c == '/' || c == '%' || c == '^' || c == '(' || c == ')')
                {
                    LastNode = CalcNode.FromChar(c);
                    m.Add(LastNode);
                }
                else
                {
                    LastNode = new CalcNode(NodeType.Variable);
                    LastNode.Value = c;
                    m.Add(LastNode);
                }
            }
            return m;
        }
        static public PolishExpression TurnToPolish(MiddleExpression f)
        {
            Stack<CalcNode> S1 = new Stack<CalcNode>();
            Stack<CalcNode> S2 = new Stack<CalcNode>();
            S1.Push(new CalcNode(NodeType.Operator));
            PolishExpression e = new PolishExpression();
            foreach (CalcNode node in f)
            {
                if (node.Type == NodeType.Operator)
                {
                    if (node.Value == '(')
                        S1.Push(node);
                    else if (node.Value == ')')
                    {
                        CalcNode LastNode = S1.Pop();
                        while (LastNode.Value != '(')
                        {
                            S2.Push(LastNode);
                            LastNode = S1.Pop();
                        }
                    }
                    else
                    {
                        if (S1.Peek().Value == '(')
                            S1.Push(node);
                        else
                        {
                            int rank = Rank(node.Value);
                            int Toprank = Rank(S1.Peek().Value);
                            if (rank <= Toprank)
                                while (!(S2.Peek().Value == '(' || Rank(S1.Peek().Value) < rank))
                                    S2.Push(S1.Pop());
                            S1.Push(node);
                        }
                    }
                }
                else
                    S2.Push(node);
            }
            if (S1.Count != 0)
                while (S1.Count > 1)
                    S2.Push(S1.Pop());
            PolishExpression p = new PolishExpression();
            while (S2.Count > 0)
                p.Push(S2.Pop());
            return p;
        }
        static public QuickCalculator TurnToMachine(PolishExpression e)
        {
            Stack<double> ns = new Stack<double>();
            CalcNode c;
            double n1, n2;
            while (e.Count > 0)
            {
                c = e.Pop();
                if (c.Type == NodeType.Num)
                    ns.Push(c.Num);
                else if (c.Type == NodeType.Variable)
                {
                    e.Push(c);
                    return new QuickCalculator(ns, e);
                }
                else
                {
                    n1 = ns.Pop(); n2 = ns.Pop();
                    ns.Push(Calc(n1, n2, c.Value));
                }
            }
            return new QuickCalculator(ns, e);
        }
        public class QuickCalculator
        {
            Stack<double> results;
            Stack<CalcNode> nodes;
            Dictionary<char, double> Dictionary = new Dictionary<char, double>();
            public QuickCalculator(Stack<double> r, Stack<CalcNode> n)
            {
                results = r;
                nodes = n;
            }
            public QuickCalculator(string s)
            {

            }
            public double this[params double[] variables]
            {
                get
                {
                    int count = 0;
                    Stack<CalcNode> RunNodes = Clone(nodes);
                    Stack<double> ns = Clone(results);
                    Dictionary<char, double> Dic = new Dictionary<char, double>();
                    foreach (char cc in Dictionary.Values)
                        Dic.Add(cc, Dictionary[cc]);
                    CalcNode c;
                    double n1, n2;
                    foreach (CalcNode cn in RunNodes)
                        if (cn.Type == NodeType.Variable)
                        {
                            double num = 0.0;
                            if (Dic.ContainsKey(cn.Value))
                                num = Dic[cn.Value];
                            else
                            {
                                num = variables[count];
                                Dic.Add(cn.Value, num);
                                count++;
                            }
                            cn.Num = num;
                            cn.Type = NodeType.Num;
                        }
                    while (RunNodes.Count > 0)
                    {
                        c = RunNodes.Pop();
                        if (c.Type == NodeType.Num)
                            ns.Push(c.Num);
                        else
                        {
                            n1 = ns.Pop(); n2 = ns.Pop();
                            ns.Push(Calc(n1, n2, c.Value));
                        }
                    }
                    return ns.Pop();
                }
            }
            Stack<CalcNode> Clone(Stack<CalcNode> s)
            {
                Stack<CalcNode> k = new Stack<CalcNode>(s);
                Stack<CalcNode> p = new Stack<CalcNode>();
                while (k.Count > 0)
                    p.Push(k.Pop().Clone() as CalcNode);
                return p;
            }
            Stack<double> Clone(Stack<double> s)
            {
                Stack<double> k = new Stack<double>(s);
                Stack<double> p = new Stack<double>();
                while (k.Count > 0)
                    p.Push(k.Pop());
                return p;
            }
            static public QuickCalculator FromString(String s)
            {
                try
                {
                    MiddleExpression m = TurnToMiddle(s);
                    PolishExpression p = TurnToPolish(m);
                    QuickCalculator q = TurnToMachine(p);
                    return q;
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
