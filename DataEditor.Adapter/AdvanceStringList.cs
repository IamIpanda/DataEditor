using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Adapter
{
    public class AdvanceStringList : FuzzyData.FuzzyArray, AdvanceData
    {
        /// <summary>
        /// 本 Advance Array 是否可用的标记
        /// </summary>
        public new bool Exists { get { return exists; } }
        /// <summary>
        /// 获取链接表
        /// </summary>
        public int StartIndex { get { return start_index; } }
        /// <summary>
        /// 获取数据
        /// </summary>
        public List<FuzzyData.FuzzyString> Data { get { return data; } }

        protected bool exists;
        protected int start_index = 0;
        protected List<FuzzyData.FuzzyString> data = new List<FuzzyData.FuzzyString>();

        public AdvanceStringList (FuzzyData.FuzzyArray array, int start_index = -1)
        {
            exists = false;
            if(array == null) return;
            this.start_index = start_index;
            for ( int i = 0; i < array.Size; i++ )
            {
                Add(array[i]);
                if ( array[i] is FuzzyData.FuzzyNil ) continue;
                else if ( array[i] is FuzzyData.FuzzyString )
                {
                    FuzzyData.FuzzyString str = array[i] as FuzzyData.FuzzyString;
                    if ( str.Text == "" ) continue;
                    if (this.start_index == -1 ) this.start_index = i;
                    data.Add(str);
                }
                else return;
            }
            exists = true;
        }
    }
}
