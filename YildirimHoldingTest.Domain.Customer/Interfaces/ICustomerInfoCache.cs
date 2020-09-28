using YildirimHoldingTest.Core.Cache.Interfaces;
using YildirimHoldingTest.Domain.Customer.Model;

namespace YildirimHoldingTest.Domain.Customer.Interfaces
{
    public interface ICustomerInfoCache : ICacheFactory<CustomerInfoModel>
    {
    }
}
