using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;

namespace Skoruba.IdentityServer4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "授权中心";
            CreateWebHost2(args).Run();
        }

        public static IWebHost CreateWebHost2(string[] args)
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
            return host;
        }
    }
}
