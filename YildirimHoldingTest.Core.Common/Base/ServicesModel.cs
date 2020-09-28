using YildirimHoldingTest.Core.Common.Helper;

namespace YildirimHoldingTest.Core.Common.Base
{
    public class ServicesResponse<TResult>
    {
        public ErrorTypeEnum ErrorType { get; set; }
        public string ResponseMessage { get; set; }
        public TResult ResultData { get; set; }
        public bool IsSuccess { get; set; }
    }
}
