using Microsoft.Extensions.Configuration;
using System;

namespace YildirimHoldingTest.Core.Common.ConfigHelper
{
    public static class AppConfigHelper
    {
        private static IConfiguration GetConfig()
        {
            var builder = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }

        public static string GetRDBConnectionString()
        {
            string conString = GetConfig().GetConnectionString("DefaultConnection");
            return conString;
        }

        public static string GetRedisServer()
        {
            var settings = GetConfig();
            if (settings["RedisServer"] != null)
            {
                var redisServers = settings["RedisServer"];
                return redisServers;
            }
            return null;
        }
        public static MongoDbConfigModel GetMongoInfo()
        {
            var settings = GetConfig();
            if (settings.GetSection("MongoDb").Exists())
                return settings.GetSection("MongoDb").Get<MongoDbConfigModel>();
            return new MongoDbConfigModel();
        }
    }
}
