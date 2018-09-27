using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skoruba.IdentityServer4.Common
{
    public static class Config
    {
        private static IConfiguration _config;
        // 一般在startup中执行此方法导入配置
        public static void ImportConfig(this IConfiguration config)
        {
            _config = config;
        }

        public static string Get(string key)
        {
            return _config[key];
        }

        public static string GetConnectionString(string name)
        {
            return _config.GetConnectionString(name);
        }

    }
}
