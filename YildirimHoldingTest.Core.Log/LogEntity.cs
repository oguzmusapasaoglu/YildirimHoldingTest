using YildirimHoldingTest.Core.Common.Base;

namespace YildirimHoldingTest.Core.Log
{
    public class LogEntity : BaseMongoEntity
    {
        public int? LogUser { get; set; }
        public int LogType { get; set; }
        public long LogDate { get; set; }
        public string LogSender { get; set; }
        public string LogMessage { get; set; }
        public string LogData { get; set; }
        public string LogException { get; set; }
        public string RequestData { get; set; }
        public long? RequestDate { get; set; }
        public string ResponseData { get; set; }
        public long? ResponseDate { get; set; }
    }
}
