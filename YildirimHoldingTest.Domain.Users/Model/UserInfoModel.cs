using YildirimHoldingTest.Core.Common.Base;

namespace YildirimHoldingTest.Domain.Users.Model
{
    public class UserInfoModel : ExtendBaseModel
    {
        public string UserNo { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
    }
}
