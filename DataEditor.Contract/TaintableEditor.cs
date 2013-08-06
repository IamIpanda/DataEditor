﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Contract
{
    public interface TaintableEditor
    {
        /// <summary>
        /// 根据 Tainted 的状态，更新自身的外观。
        /// </summary>
        void Putt();
    }
    public interface TaintableSingleEditor : TaintableEditor
    {
        TaintState Tainted { get; set; }
    }
    public interface TaintableMultiEditor : TaintableEditor
    {
        TaintCollection Tainted { get; }
    }
    public interface TaintableTrigger
    {
        /// <summary>
        /// 当控件值被污染时，触发此事件。
        /// </summary>
        event EventHandler Taint;
    }
    public interface TaintCollection
    {
        TaintState this[int index] { get; set; }
        int Count { get; }
    }
    /// <summary>
    /// 记录了数据的污染状态的值
    /// </summary>
    public enum TaintState
    {
        /// <summary>
        /// 数据从文件中被读取，未经过修改
        /// </summary>
        UnTainted,
        /// <summary>
        /// 单值数据文件被修改，尚未保存
        /// </summary>
        Tainted,
        /// <summary>
        /// 由于子数据元被修改，被标记为被修改，且尚未保存
        /// </summary>
        ChildTainted,
        /// <summary>
        /// 由于整个被替换，被标记为被修改，且尚未保存
        /// </summary>
        FullReplaced,
        /// <summary>
        /// 此数据是被添加的，且尚未保存
        /// </summary>
        Added,
        /// <summary>
        /// 此数据已经被修改并保存到文件了
        /// </summary>
        Saved
    }
}
