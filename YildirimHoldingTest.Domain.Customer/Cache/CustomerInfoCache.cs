using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using YildirimHoldingTest.Core.Cache.Manager;
using YildirimHoldingTest.Core.Common.Helper;
using YildirimHoldingTest.Core.Data.Interfaces;
using YildirimHoldingTest.Core.Log;
using YildirimHoldingTest.Domain.Customer.Interfaces;
using YildirimHoldingTest.Domain.Customer.Model;
using YildirimHoldingTest.Domain.InvoiceDomain.Entities;
using YildirimHoldingTest.Core.Common.Map;
using System.Reflection;
using YildirimHoldingTest.Core.Common.Exceptions;

namespace YildirimHoldingTest.Domain.Customer.Cache
{
    public class CustomerInfoCache : CacheManager, ICustomerInfoCache
    {
        private ILogFactory logger;
        private IConnectionFactory connectionFactory;
        private string baseCacheKey => "CustomerInfo";
        public CustomerInfoCache(ILogFactory _logFactory, IConnectionFactory _connectionFactory)
        {
            logger = _logFactory;
            connectionFactory = _connectionFactory;
        }
        public IEnumerable<CustomerInfoModel> GetAllCachedData()
        {
            var key = GetCacheKey(baseCacheKey);
            var result = GetCachedData<CustomerInfoModel>(key);
            if (result == null || !result.Any())
            {
                IEnumerable<CustomerInfoModel> data = GetData<CustomerInfoModel>();
                FillCacheData(key, data);
                return data;
            }
            return result;
        }
        protected override IEnumerable<CustomerInfoModel> GetData<CustomerInfoModel>()
        {
            try
            {
                using (var conn = connectionFactory.CreateConnection())
                {
                    if (conn.State == System.Data.ConnectionState.Closed)
                        conn.Open();
                    var data = conn.GetAll<CustomerInfoEntity>().Where(q => q.ActivationStatus == ActivationStatusEnum.Active.ToInt());
                    if (data != null && data.Any())
                        return Mapper.Map<IEnumerable<CustomerInfoEntity>, IEnumerable<CustomerInfoModel>>(data);
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
