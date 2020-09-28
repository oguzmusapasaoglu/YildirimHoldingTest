using System;
namespace YildirimHoldingTest.Domain.InvoiceDomain.Model
{
    public class InvoiceHeaderReponseModel
    {
        public string InvoiceNo { get; set; }
        public string CustomerNo { get; set; }
        public string Address { get; set; }
        public decimal GrandTotal { get; set; }
        public string Currency { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
