using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using Skoruba.IdentityServer4.Admin.Helpers;

namespace Skoruba.IdentityServer4.Admin
{
    public class Program
    {
        private const string SeedArgs = "/seed";

        public static async Task Main(string[] args)
        {
            //var seed = args.Any(x => x == SeedArgs);
            //if (seed) args = args.Except(new[] { SeedArgs }).ToArray();

            //var host = BuildWebHost(args);

            //// Uncomment this to seed upon startup, alternatively pass in `dotnet run /seed` to seed using CLI
            //// await DbMigrationHelpers.EnsureSeedData(host);
            //if (seed)
            //{
            //    await DbMigrationHelpers.EnsureSeedData(host);
            //}

            //host.Run();


            CreateWebHost(args).Result.Run();
        }

        public static async Task<IWebHost> CreateWebHost(string[] args)
        {
            var config = new ConfigurationBuilder().AddCommandLine(args).Build();
            IWebHostBuilder builder = WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
            string seeddata = config["init"];
            string port = config["port"];

            if (!string.IsNullOrWhiteSpace(port))
            {
                builder.UseUrls($"http://*:{port}");
            }
            var host = builder.Build();

            if (seeddata == "initdatabase")
            {
               await DbMigrationHelpers.EnsureSeedData(host);
            }
            return host;
        }

        //public static IWebHost BuildWebHost(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)
        //           .UseKestrel(c => c.AddServerHeader = false)
        //           .UseStartup<Startup>()
        //           .UseSerilog()
        //           .Build();
    }
}