using System.ComponentModel.DataAnnotations.Schema;
using YildirimHoldingTest.Core.Common.Base;

namespace YildirimHoldingTest.Domain.InvoiceDomain.Entities
{
    [Table("Invoice.InvoiceHeader")]
    public class InvoiceHeaderEntity : ExtendBaseEntity
    {
        public string InvoiceNo { get; set; }
        public int CustomerId { get; set; }
        public decimal GrandTotal { get; set; }
        public string Currency { get; set; }
    }
}
