﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Help
{
    public class TaintHelper
    {
        static public void OnPutt(System.Windows.Forms.Label Label, Contract.TaintState State)
        {
            if (Label == null) return;
            Label.ForeColor = TaintOptions.DefaultColors[State];
        }
    }
    public static class TaintRecord
    {
        static HashCollection<FuzzyData.FuzzyObject, Contract.TaintState> single
            = new HashCollection<FuzzyData.FuzzyObject,Contract.TaintState>();
        static HashCollection<FuzzyData.FuzzyObject, Contract.TaintCollection> multi
            = new HashCollection<FuzzyData.FuzzyObject, Contract.TaintCollection>();
        static public HashCollection<FuzzyData.FuzzyObject, Contract.TaintState> Single { get { return single; } }
        static public HashCollection<FuzzyData.FuzzyObject, Contract.TaintCollection> Multi { get { return multi; } }
    }
    public static class TaintOptions
    {
        static public Dictionary<DataEditor.Contract.TaintState, System.Drawing.Color> DefaultColors
            = new Dictionary<Contract.TaintState, System.Drawing.Color>();
        static TaintOptions()
        {
            Load();
        }
        static void Load()
        {
            object ob = InitalizedObjectEditorHelper.GetOption(typeof(TaintOptions));
            if (ob == null) return;
            var target = ob as Dictionary<DataEditor.Contract.TaintState, System.Drawing.Color>;
            if (target == null) return;
            DefaultColors = target;
        }
        static void Save()
        {
            InitalizedObjectEditorHelper.SetOption(typeof(TaintOptions), DefaultColors);
        }
    }
}
