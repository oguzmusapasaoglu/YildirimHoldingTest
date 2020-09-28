using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using YildirimHoldingTest.Core.Common.Base;
using YildirimHoldingTest.Core.Common.Exceptions;
using YildirimHoldingTest.Core.Common.Helper;
using YildirimHoldingTest.Core.Log;
using YildirimHoldingTest.Domain.Customer.Interfaces;
using YildirimHoldingTest.Domain.Customer.Model;
using YildirimHoldingTest.Domain.InvoiceDomain.Interfaces;

namespace YildirimHoldingTest.Domain.Customer.Manager
{
    public class CustomerInfoManager : ICustomerInfoManager
    {
        private ILogFactory logger;
        private ICustomerInfoCache cache;

        public CustomerInfoManager(
            ICustomerInfoCache _cache,
            ILogFactory _logger
            )
        {
            logger = _logger;
            cache = _cache;
        }
        public ServicesResponse<IEnumerable<CustomerInfoModel>> GetAllDataByFilter(int? customerId = null, string customerNo = "")
        {
            var methodName = MethodBase.GetCurrentMethod().GetMethodName();
            try
            {
                var data = cache.GetAllCachedData().AsQueryable();
                var predicate = PredicateBuilderHelper.True<CustomerInfoModel>();

                #region Filter Condition
                if (!customerId.IsNullOrLessOrEqToZero())
                    predicate = predicate.Or(q => q.Id == customerId);
                else if (!customerNo.IsNullOrEmpty())
                    predicate = predicate.Or(q => q.CustomerNo == customerNo);
                #endregion
                var result = data.Where(predicate).AsEnumerable();
                return GlobalHelper.CreateServicesResponse(result);
            }
            catch (FattalDbException ex)
            {
                logger.AddErrorLog(LogTypeEnum.Error, methodName, null, ex.ExceptionMessageProp, ex);
                return GlobalHelper.CreateServicesErrorResponse<IEnumerable<CustomerInfoModel>>(ex.ExceptionMessageProp, ex.ErrorTypeProp);
            }
            catch (KnownException ex)
            {
                return GlobalHelper.CreateServicesErrorResponse<IEnumerable<CustomerInfoModel>>(ex.ExceptionMessageProp, ex.ErrorTypeProp);
            }
            catch (Exception ex)
            {
                logger.AddErrorLog(LogTypeEnum.Error, methodName, null, ex.Message, ex);
                return GlobalHelper.CreateServicesErrorResponse<IEnumerable<CustomerInfoModel>>();
            }
        }
    }
}
