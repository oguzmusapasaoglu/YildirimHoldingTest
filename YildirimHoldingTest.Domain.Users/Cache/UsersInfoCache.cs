using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using YildirimHoldingTest.Core.Cache.Manager;
using YildirimHoldingTest.Core.Common.Helper;
using YildirimHoldingTest.Core.Data.Interfaces;
using YildirimHoldingTest.Core.Log;
using YildirimHoldingTest.Domain.Users.Interfaces;
using YildirimHoldingTest.Domain.Users.Model;
using YildirimHoldingTest.Domain.InvoiceDomain.Entities;
using YildirimHoldingTest.Core.Common.Map;
using System.Reflection;
using YildirimHoldingTest.Core.Common.Exceptions;

namespace YildirimHoldingTest.Domain.Users.Cache
{
    public class UsersInfoCache : CacheManager, IUsersInfoCache
    {
        private ILogFactory logger;
        private IConnectionFactory connectionFactory;
        private string baseCacheKey => "UsersInfo";
        public UsersInfoCache(ILogFactory _logFactory, IConnectionFactory _connectionFactory)
        {
            logger = _logFactory;
            connectionFactory = _connectionFactory;
        }
        public IEnumerable<UserInfoModel> GetAllCachedData()
        {
            var key = GetCacheKey(baseCacheKey);
            var result = GetCachedData<UserInfoModel>(key);
            if (result == null || !result.Any())
            {
                IEnumerable<UserInfoModel> data = GetData<UserInfoModel>();
                FillCacheData(key, data);
                return data;
            }
            return result;
        }
        protected override IEnumerable<UserInfoModel> GetData<UserInfoModel>()
        {
            try
            {
                using (var conn = connectionFactory.CreateConnection())
                {
                    if (conn.State == System.Data.ConnectionState.Closed)
                        conn.Open();
                    var data = conn.GetAll<UsersInfoEntity>().Where(q => q.ActivationStatus == ActivationStatusEnum.Active.ToInt());
                    if (data != null && data.Any())
                        return Mapper.Map<IEnumerable<UsersInfoEntity>, IEnumerable<UserInfoModel>>(data);
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                var methotName = MethodBase.GetCurrentMethod().GetMethodName();
                logger.AddFattalLog(methotName, ExceptionMessageHelper.UnexpectedCacheError, "", ex);
                throw new KnownException(ErrorTypeEnum.CahceGeneralException, methotName, ex);
            }
        }
    }
}
