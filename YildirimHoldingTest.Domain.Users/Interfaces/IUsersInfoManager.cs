using System.Collections.Generic;
using YildirimHoldingTest.Core.Common.Base;
using YildirimHoldingTest.Domain.Users.Model;

namespace YildirimHoldingTest.Domain.Users.Interfaces
{
    public interface IUsersInfoManager
    {
        ServicesResponse<IEnumerable<UserInfoModel>> GetAllDataByFilter(UserInfoRequestModel requestModel);

        ServicesResponse<IEnumerable<UserInfoModel>> GetAllData();
    }
}
