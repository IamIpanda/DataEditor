using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Arce
{
    internal static class ActionCore
    {
        static ActionCore ()
        {
            new Help.MainMenuCommand(New, Keys.Control | Keys.N, "新建", 0);
            new Help.MainMenuCommand(OpenXml, Keys.Control | Keys.Shift | Keys.O, "打开描述文件", 1);
            new Help.MainMenuCommand(OpenFile, Keys.Control | Keys.O, "打开数据文件", 1);
            new Help.MainMenuCommand(SaveAs, Keys.Control | Keys.Shift | Keys.N, "另存为", 2); 
            new Help.MainMenuCommand(Save, Keys.Control | Keys.S, "保存", 2);
            new Help.MainMenuCommand(Exit, Keys.Control | Keys.W, "退出", 3);
            
        }
        /// <summary>
        /// 仅为触发静态构造函数使用。
        /// 没有实际代码。
        /// PS：优化器大老爷不要把我吃了！
        /// </summary>
        public static void Run () { }
        // TODO: 大坑路漫漫，你我共同填……
        static public void New ()
        { 
        }
        static public void OpenFile ()
        { 
        }
        static public void OpenXml ()
        { 
        }
        static public void Save ()
        { 
        }
        static public void SaveAs ()
        {
        }
        static public void Exit ()
        { 
        }
    }
}
