using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace DataEditor.Help
{
    public delegate void RunMainMenuCommand();
    public delegate void RunArrayMenuCommand(Adapter.AdvanceCollectNameArray Array);

    /// <summary>
    /// 松散地托管菜单指令的类。
    /// 承诺：T一定是一个委托类型，派生自：System.MulticastDelegate
    /// </summary>
    /// <typeparam name="T">指定的委托类型</typeparam>
    public class MenuCommand<T> : Contract.MenuCommand
    {
        public T Run { get; set; }
        public string Name { get; set; }
        public int Group { get; set; }
        public System.Windows.Forms.Keys Key { get; set; }
    
        public MenuCommand (T run, System.Windows.Forms.Keys key, string name = "", int group = 0)
        {
            Run = run;
            Key = key;
            Name = name;
            Group = group;
            commands.Add(this);
        }
        protected static List<MenuCommand<T>> commands = new List<MenuCommand<T>>();
        public static List<MenuCommand<T>> Commands { get { return commands; } }
        public static void Sort () { commands.Sort((x, y) => x.Group - y.Group); }
    }

    public class MainMenuCommand : MenuCommand<RunMainMenuCommand> 
    {
        public MainMenuCommand (RunMainMenuCommand run, System.Windows.Forms.Keys key, string name = "", int group = 0) : base(run, key, name, group) { }
    }
    public class ArrayMenuCommand : MenuCommand<RunArrayMenuCommand>
    {
      public ArrayMenuCommand (RunArrayMenuCommand run, System.Windows.Forms.Keys key, string name = "", int group = 0) : base(run, key, name, group) { }
    }
    
}
