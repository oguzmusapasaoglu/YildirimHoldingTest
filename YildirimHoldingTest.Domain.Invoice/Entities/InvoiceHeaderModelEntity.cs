using YildirimHoldingTest.Core.Common.Base;

namespace YildirimHoldingTest.Domain.InvoiceDomain.Entities
{
    public class InvoiceHeaderModelEntity : BaseEntity
    {
        public string InvoiceNo { get; set; }
        public string CustomerNo { get; set; }
        public string Address { get; set; }
        public decimal GrandTotal { get; set; }
        public string Currency { get; set; }
        public string CreatedBy { get; set; }
    }
}
