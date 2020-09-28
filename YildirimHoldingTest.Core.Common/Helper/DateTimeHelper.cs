using System;
using System.Globalization;

namespace YildirimHoldingTest.Core.Common.Helper
{
    public static class DateTimeHelper
    {
        public static DateTime? ToDateTime(this long numberDate)
        {
            DateTime result;
            if (numberDate == 0)
                return null;
            if (DateTime.TryParseExact(numberDate.ToString(), new[] { "yyyyMMddHHmmss", "yyyyMMddHHmm" }, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out result))
                return result;
            return null;
        }
        public static long Now => DateTime.Now.ToLong();
        public static int GetDay(this long dt) => dt.ToDateTime().Value.Day;
    }
}
