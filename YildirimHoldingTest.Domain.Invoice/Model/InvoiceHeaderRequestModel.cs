using System;

namespace YildirimHoldingTest.Domain.InvoiceDomain.Model
{
    public class InvoiceHeaderRequestModel
    {
        public string InvoiceNo { get; set; }
        public string CustomerNo { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
