namespace YildirimHoldingTest.Core.Common.Helper
{
    public enum ActivationStatusEnum
    {
        Active = 1,
        Passive = 2,
        Pending = 3
    }
    public enum ErrorTypeEnum
    {
        GeneralExeption,
        UnexpectedExeption,
        DbOperationException,
        WarningException,
        CacheConnectExeption,
        CacheExpireExeption,
        CacheEndExeption,
        CacheNotExsistExeption,
        CahceGeneralException
    }
    public enum LogTypeEnum
    {
        Info = 1,
        Warn = 2,
        Error = 3,
        Fattal = 4
    }
    public static class EnumHelper
    {
        public static ActivationStatusEnum ToActivationStatus(this int value)
        {
            return (ActivationStatusEnum)value;
        }

        public static int ToInt(this ActivationStatusEnum value)
        {
            return (int)value;
        }
    }
}
