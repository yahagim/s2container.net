using System;

namespace Seasar.Tests.Dxo
{
    /// <summary>
    /// 日付をDateTimeで持つ
    /// </summary>
    public class DateValueDateTime
    {
        private DateTime _dateValue;

        public DateTime DateValue
        {
            get { return _dateValue; }
            set { _dateValue = value; }
        }
    }
}