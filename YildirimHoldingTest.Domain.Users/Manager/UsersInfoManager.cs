using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using YildirimHoldingTest.Core.Common.Base;
using YildirimHoldingTest.Core.Common.Exceptions;
using YildirimHoldingTest.Core.Common.Helper;
using YildirimHoldingTest.Core.Log;
using YildirimHoldingTest.Domain.Users.Interfaces;
using YildirimHoldingTest.Domain.Users.Model;

namespace YildirimHoldingTest.Domain.Users.Manager
{
    public class UsersInfoManager : IUsersInfoManager
    {
        private ILogFactory logger;
        private IUsersInfoCache cache;

        public UsersInfoManager(
            IUsersInfoCache _cache,
            ILogFactory _logger
            )
        {
            logger = _logger;
            cache = _cache;
        }

        public ServicesResponse<IEnumerable<UserInfoModel>> GetAllData()
        {
            var methodName = MethodBase.GetCurrentMethod().GetMethodName();
            try
            {
                var data = cache.GetAllCachedData();
                return GlobalHelper.CreateServicesResponse(data);
            }
            catch (FattalDbException ex)
            {
                logger.AddErrorLog(LogTypeEnum.Error, methodName, null, ex.ExceptionMessageProp, ex);
                return GlobalHelper.CreateServicesErrorResponse<IEnumerable<UserInfoModel>>(ex.ExceptionMessageProp, ex.ErrorTypeProp);
            }
            catch (KnownException ex)
            {
                return GlobalHelper.CreateServicesErrorResponse<IEnumerable<UserInfoModel>>(ex.ExceptionMessageProp, ex.ErrorTypeProp);
            }
            catch (Exception ex)
            {
                logger.AddErrorLog(LogTypeEnum.Error, methodName, null, ex.Message, ex);
                return GlobalHelper.CreateServicesErrorResponse<IEnumerable<UserInfoModel>>();
            }
        }

        public ServicesResponse<IEnumerable<UserInfoModel>> GetAllDataByFilter(UserInfoRequestModel requestModel)
        {
            var methodName = MethodBase.GetCurrentMethod().GetMethodName();
            try
            {
                var data = cache.GetAllCachedData().AsQueryable();
                var predicate = PredicateBuilderHelper.True<UserInfoModel>();

                #region Filter Condition
                if (!requestModel.UserId.IsNullOrLessOrEqToZero())
                    predicate = predicate.Or(q => q.Id == requestModel.UserId);

                if (!requestModel.UserNo.IsNullOrEmpty())
                    predicate = predicate.Or(q => q.UserNo == requestModel.UserNo);

                if (!requestModel.UserName.IsNullOrEmpty())
                    predicate = predicate.Or(q => q.UserName.Contains(requestModel.UserName));

                if (!requestModel.FullName.IsNullOrEmpty())
                    predicate = predicate.Or(q => q.FullName.Contains(requestModel.FullName));
                #endregion

                var result = data.Where(predicate).AsEnumerable();
                return GlobalHelper.CreateServicesResponse(result);
            }
            catch (FattalDbException ex)
            {
                logger.AddErrorLog(LogTypeEnum.Error, methodName, null, ex.ExceptionMessageProp, ex);
                return GlobalHelper.CreateServicesErrorResponse<IEnumerable<UserInfoModel>>(ex.ExceptionMessageProp, ex.ErrorTypeProp);
            }
            catch (KnownException ex)
            {
                return GlobalHelper.CreateServicesErrorResponse<IEnumerable<UserInfoModel>>(ex.ExceptionMessageProp, ex.ErrorTypeProp);
            }
            catch (Exception ex)
            {
                logger.AddErrorLog(LogTypeEnum.Error, methodName, null, ex.Message, ex);
                return GlobalHelper.CreateServicesErrorResponse<IEnumerable<UserInfoModel>>();
            }
        }
    }
}
