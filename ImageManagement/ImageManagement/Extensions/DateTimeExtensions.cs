using System;
using System.Globalization;

namespace ImageManagement.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToPersianDate(this DateTime date)
        {
            PersianCalendar jc = new PersianCalendar();
            return string.Format("{0:0000}-{1:00}-{2:00}", jc.GetYear(date), jc.GetMonth(date), jc.GetDayOfMonth(date));
        }
    }
}
