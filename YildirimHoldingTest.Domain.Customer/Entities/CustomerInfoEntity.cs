using Dapper.Contrib.Extensions;
using YildirimHoldingTest.Core.Common.Base;

namespace YildirimHoldingTest.Domain.InvoiceDomain.Entities
{
    [Table("Customer.CustomerInfo")]
    public class CustomerInfoEntity : ExtendBaseEntity
    {
        public string CustomerNo { get; set; }
        public string CustomerName { get; set; }
        public string CutomerCity { get; set; }
    }
}
