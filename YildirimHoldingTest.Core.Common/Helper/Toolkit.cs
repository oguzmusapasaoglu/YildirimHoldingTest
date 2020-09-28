﻿namespace YildirimHoldingTest.Core.Common.Helper
{
    public static class Toolkit
    {
        public static int ToInt(this bool value)
        {
            return (value) ? 1 : 0;
        }
        public static int ToInt(this object value)
        {
            int ParmOut;
            return int.TryParse(value.ToString(), out ParmOut)
                ? ParmOut
                : 0;
        }
        public static long ToLong(this object value)
        {
            long ParmOut;
            return long.TryParse(value.ToString(), out ParmOut)
                ? ParmOut
                : 0;
        }
        public static bool IsNullOrEmpty(this object value)
        {
            return (value == null || value.ToString().Trim() == string.Empty);
        }
        public static bool IsNullOrLessOrEqToZero(this object value)
        {
            return (value == null || value.ToLong() <= 0);
        }
    }
}
