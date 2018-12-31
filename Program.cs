using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using WebApp1.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Hosting.WindowsServices;

namespace WebApp1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var isService = !(Debugger.IsAttached || args.Contains("--console"));

            //if (isService)//for Deploy
            //{
            //    var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
            //    var pathToContentRoot = Path.GetDirectoryName(pathToExe);
            //    Directory.SetCurrentDirectory(pathToContentRoot);
            //}

            var builder = CreateWebHostBuilder(args/*.Where(arg => arg != "--console").ToArray()*/);
            
            var host = builder
                .Build();

            using(var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();

                var config = host.Services.GetRequiredService<IConfiguration>();
                var testUserPw = config["SeedUserPW"];
                try
                {
                    SeedData.Initialize(services, testUserPw).Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex.Message, "An error occurred seeding the DB.");
                }
            }

            //if (isService)
            //{
            //    host.RunAsService();//for Deploy
            //}
            //else
            //{
                host.Run();
            //}
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
