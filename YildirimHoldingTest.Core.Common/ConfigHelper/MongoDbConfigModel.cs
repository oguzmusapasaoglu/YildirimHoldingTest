namespace YildirimHoldingTest.Core.Common.ConfigHelper
{
    public class MongoDbConfigModel
    {
        public string ConnectionString { get; set; }
        public string LogDbName { get; set; }
        public string ErrorLogCollection { get; set; }
        public string InfoLogCollection { get; set; }
    }
}
