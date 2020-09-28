using System.Collections.Generic;
using YildirimHoldingTest.Core.Common.Base;
using Microsoft.AspNetCore.Mvc;
using YildirimHoldingTest.Core.Common.Helper;
using YildirimHoldingTest.Domain.InvoiceDomain.Interfaces;
using YildirimHoldingTest.Domain.InvoiceDomain.Model;

namespace YildirimHoldingTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvoiceHeaderController : ControllerBase
    {
        private IInvoiceHeaderManager manager;
        public InvoiceHeaderController(
            IInvoiceHeaderManager _manager)
        {
            manager = _manager;
        }

        [HttpGet]
        public ServicesResponse<IEnumerable<InvoiceHeaderReponseModel>> Get(string invoiceNo, string customerNo, long startDate = 0, long endDate = 0)
        {
            return manager.GetAllDataByFilterV2(new InvoiceHeaderRequestModel { InvoiceNo = invoiceNo, CustomerNo = customerNo, StartDate = startDate.ToDateTime(), EndDate = endDate.ToDateTime() });
        }
    }
}
