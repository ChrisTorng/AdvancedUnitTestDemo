using AdvancedUnitTestDemo.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace AdvancedUnitTestDemo
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            CreateDbIfNotExists();

            host.Run();
        }

        private static void CreateDbIfNotExists()
        {
            using var schoolContext = new SchoolContext();
            DbInitializer.Initialize(schoolContext);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
