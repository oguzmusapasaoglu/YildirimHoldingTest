using System;
using System.Collections.Generic;
using ServiceStack.Redis;
using System.Linq;
using System.Reflection;
using YildirimHoldingTest.Core.Common.Base;
using YildirimHoldingTest.Core.Common.Helper;
using YildirimHoldingTest.Core.Common.Exceptions;

namespace YildirimHoldingTest.Core.Cache.Manager
{
    public abstract class CacheManager : RedisManager
    {
        private IRedisClient client;
        protected IEnumerable<string> GetAllKeys()
        {
            string methotName = MethodBase.GetCurrentMethod().GetMethodName();
            try
            {
                client = GetRedisClient();
                var result = client.GetAllKeys();
                return result.AsEnumerable();
            }
            catch (RedisException ex)
            {
                throw new KnownException(ErrorTypeEnum.CacheConnectExeption, methotName, ex); ;
            }
            catch (Exception ex)
            {
                throw new KnownException(ErrorTypeEnum.CahceGeneralException, methotName, ex);
            }
        }
        protected IEnumerable<TData> GetCachedData<TData>(string cacheKey = "") where TData : BaseModel
        {
            string methotName = MethodBase.GetCurrentMethod().GetMethodName();
            try
            {
                client = GetRedisClient();
                var result = client.Get<IEnumerable<TData>>(cacheKey);
                return result;
            }
            catch (RedisException ex)
            {

                throw new KnownException(ErrorTypeEnum.CacheConnectExeption, methotName, ExceptionMessageHelper.UnexpectedCacheError);
            }
            catch (Exception ex)
            {

                throw new KnownException(ErrorTypeEnum.CahceGeneralException, methotName, ex);
            }
        }
        protected void FillCacheData<TData>(string cacheKey, IEnumerable<TData> data, DateTime? expiredDate = null) where TData : BaseModel
        {
            string methotName = MethodBase.GetCurrentMethod().GetMethodName();
            try
            {
                client = GetRedisClient();
                if (client.GetAllKeys().Any(q => q == cacheKey))
                    client.Remove(cacheKey);
                if (!expiredDate.HasValue)
                    expiredDate = DateTime.Now.AddDays(1);

                client.Set(cacheKey, data, expiredDate.Value);
            }
            catch (RedisException ex)
            {
                throw new KnownException(ErrorTypeEnum.CacheConnectExeption, methotName, ex); ;
            }
            catch (Exception ex)
            {

                throw new KnownException(ErrorTypeEnum.CahceGeneralException, methotName, ex);
            }
        }
        protected bool AddSingleDataByKey<TData>(string cacheKey, TData entity, DateTime expiredDate) where TData : BaseModel
        {
            string methotName = MethodBase.GetCurrentMethod().GetMethodName();
            try
            {
                client = GetRedisClient();
                var data = GetCachedData<TData>(cacheKey).ToList();
                if (data.Any())
                {
                    data.Add(entity);
                    client.Remove(cacheKey);
                    client.Add(cacheKey, data, expiredDate);
                    return true;
                }
                return false;
            }
            catch (RedisException ex)
            {
                throw new KnownException(ErrorTypeEnum.CacheConnectExeption, methotName, ex); ;
            }
            catch (Exception ex)
            {

                throw new KnownException(ErrorTypeEnum.CahceGeneralException, methotName, ex);
            }
        }
        protected TData GetDataByKey<TData>(string cacheKey) where TData : BaseModel
        {
            string methotName = MethodBase.GetCurrentMethod().GetMethodName();
            try
            {
                client = GetRedisClient();
                return client.Get<TData>(cacheKey);
            }
            catch (RedisException ex)
            {
                throw new KnownException(ErrorTypeEnum.CacheConnectExeption, methotName, ex); ;
            }
            catch (Exception ex)
            {

                throw new KnownException(ErrorTypeEnum.CahceGeneralException, methotName, ex);
            }
        }
        protected TData GetDataById<TData>(int Id) where TData : BaseModel
        {
            string methotName = MethodBase.GetCurrentMethod().GetMethodName();
            try
            {
                client = GetRedisClient();
                return client.GetById<TData>(Id);
            }
            catch (RedisException ex)
            {
                throw new KnownException(ErrorTypeEnum.CacheConnectExeption, methotName, ex); ;
            }
            catch (Exception ex)
            {

                throw new KnownException(ErrorTypeEnum.CahceGeneralException, methotName, ex);
            }
        }
        protected bool RemoveDataByKey(string cacheKey)
        {
            string methotName = MethodBase.GetCurrentMethod().GetMethodName();
            try
            {
                client = GetRedisClient();
                return client.Remove(cacheKey);
            }
            catch (RedisException ex)
            {
                throw new KnownException(ErrorTypeEnum.CacheConnectExeption, methotName, ex); ;
            }
            catch (Exception ex)
            {

                throw new KnownException(ErrorTypeEnum.CahceGeneralException, methotName, ex);
            }
        }
        protected bool IsExsistByKey(string cacheKey)
        {
            string methotName = MethodBase.GetCurrentMethod().GetMethodName();
            try
            {
                client = GetRedisClient();
                var AllKey = client.GetAllKeys();
                if (AllKey == null || AllKey.Count <= 0)
                    return false;

                return AllKey.Exists(q => q == cacheKey);
            }
            catch (RedisException ex)
            {
                throw new KnownException(ErrorTypeEnum.CacheConnectExeption, methotName, ex); ;
            }
            catch (Exception ex)
            {

                throw new KnownException(ErrorTypeEnum.CahceGeneralException, methotName, ex);
            }
        }
        protected string GetCacheKey(string baseName)
        {
            string methotName = MethodBase.GetCurrentMethod().GetMethodName();
            try
            {
                return baseName + (DateTimeHelper.Now.GetDay() + 1).ToString();
            }
            catch (Exception ex)
            {
                throw new KnownException(ErrorTypeEnum.CahceGeneralException, methotName, ex);
            }
        }
        protected abstract IEnumerable<TData> GetData<TData>() where TData : BaseModel;
    }
}
