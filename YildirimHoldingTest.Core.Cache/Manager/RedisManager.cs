using ServiceStack.Redis;
using YildirimHoldingTest.Core.Common.ConfigHelper;
using YildirimHoldingTest.Core.Common.Exceptions;

namespace YildirimHoldingTest.Core.Cache.Manager
{
    public abstract class RedisManager
    {
        protected internal RedisClient redisClient;
        protected internal void ConfigRedis()
        {
            var redisHost = AppConfigHelper.GetRedisServer();
            redisClient = new RedisClient(redisHost);
        }
        protected internal RedisClient GetRedisClient()
        {
            try
            {
                var redisHost = AppConfigHelper.GetRedisServer();
                redisClient = new RedisClient(redisHost);
                return redisClient;
            }
            catch (System.Exception ex)
            {
                throw new RedisException(ExceptionMessageHelper.UnexpectedCacheError, ex);
            }
        }
    }
}
