using System.Collections.Generic;

using YildirimHoldingTest.Core.Common.Base;
using YildirimHoldingTest.Domain.Customer.Model;

namespace YildirimHoldingTest.Domain.InvoiceDomain.Interfaces
{
    public interface ICustomerInfoManager
    {
        ServicesResponse<IEnumerable<CustomerInfoModel>> GetAllDataByFilter(int? customerId = null, string customerNo = "");
    }
}
