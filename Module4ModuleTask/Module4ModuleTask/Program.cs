using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Module4ModuleTask.Data;
using Module4ModuleTask.Services;
using Module4ModuleTask.Services.Abstractions;

namespace Module4ModuleTask
{
    public class Program
    {
        private static void Main()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection, configuration);

            var provider = serviceCollection.BuildServiceProvider();

            var app = provider.GetService<App>();
            app!.Start();
        }

        private static void ConfigureServices(ServiceCollection serviceCollection, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            serviceCollection.AddDbContext<ApplicationDbContext>(options
                => options.UseSqlServer(connectionString));
            serviceCollection.AddScoped<IDbContextWrapper<ApplicationDbContext>, DbContextWrapper<ApplicationDbContext>>();

            serviceCollection
                .AddLogging(configure => configure.AddConsole())
                .AddTransient<App>();
        }
    }
}