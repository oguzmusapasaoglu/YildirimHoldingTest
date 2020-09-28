using System.Collections.Generic;
using YildirimHoldingTest.Core.Common.Base;
using YildirimHoldingTest.Domain.InvoiceDomain.Model;

namespace YildirimHoldingTest.Domain.InvoiceDomain.Interfaces
{
    public interface IInvoiceHeaderManager
    {
        ServicesResponse<IEnumerable<InvoiceHeaderReponseModel>> GetAllDataByFilterV1(InvoiceHeaderRequestModel request);
        ServicesResponse<IEnumerable<InvoiceHeaderReponseModel>> GetAllDataByFilterV2(InvoiceHeaderRequestModel request);
    }
}
