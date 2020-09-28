
using YildirimHoldingTest.Core.Common.Base;

namespace YildirimHoldingTest.Domain.Customer.Model
{
    public class CustomerInfoModel : ExtendBaseModel
    {
        public string CustomerNo { get; set; }
        public string CustomerName { get; set; }
        public string CutomerCity { get; set; }
    }
}
