using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Skoruba.IdentityServer4.Attribute
{
    public class AttributeConfig
    {
        private static IConfiguration Configuration;

        public static string Get(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                if (Configuration == null)
                {
                    var builder = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                       // .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                       // .AddEnvironmentVariables();
                    Configuration = builder.Build();
                }                
                return Configuration[key];
            }
            return string.Empty;
        }
    }
}
