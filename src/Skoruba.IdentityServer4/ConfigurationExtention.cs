using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Skoruba.IdentityServer4.Common;
using Skoruba.IdentityServer4.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Skoruba.IdentityServer4
{
    public static class ConfigurationExtention
    {
        /// <summary>
        /// 注册IdentityServer
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegitsterIdentityServer(this IServiceCollection services)
        {
            try
            {
                string connectionString = Config.GetConnectionString("AuthDB");
                var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

                services.AddIdentityServer()
                        .AddDeveloperSigningCredential()
                        .AddTestUsers(IdentityServerConfig.GetUsers())
                       // .AddResourceOwnerValidator<CustomResourceOwnerPasswordValidator>()
                        .AddConfigurationStore(options =>
                        {
                            options.ConfigureDbContext = builder =>
                                builder.UseSqlServer(connectionString,
                                    sql => sql.MigrationsAssembly(migrationsAssembly));
                        })
                        .AddOperationalStore(options =>
                        {
                            options.ConfigureDbContext = builder =>
                                builder.UseSqlServer(connectionString,
                                    sql => sql.MigrationsAssembly(migrationsAssembly));
                            options.EnableTokenCleanup = true;
                            options.TokenCleanupInterval = 30;
                        });
            }
            catch (Exception)
            {

            }
            return services;
        }
    }
}
