using AdvancedUnitTestDemo.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SchoolDatabase;

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
            using var schoolContext = new SchoolContext(new DbContextOptionsBuilder<SchoolContext>()
                .UseSqlServer(Startup.Configuration.GetConnectionString("SchoolContext"))
                .Options);
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
