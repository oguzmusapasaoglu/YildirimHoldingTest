using YildirimHoldingTest.Core.Cache.Interfaces;
using YildirimHoldingTest.Domain.Users.Model;

namespace YildirimHoldingTest.Domain.Users.Interfaces
{
    public interface IUsersInfoCache : ICacheFactory<UserInfoModel>
    {
    }
}
