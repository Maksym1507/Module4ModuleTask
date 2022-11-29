using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Module4ModuleTask.Data;
using Module4ModuleTask.Repositories;
using Module4ModuleTask.Repositories.Abstractions;
using Module4ModuleTask.Services;
using Module4ModuleTask.Services.Abstractions;

namespace Module4ModuleTask
{
    public class Program
    {
        private static async Task Main()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection, configuration);

            var provider = serviceCollection.BuildServiceProvider();

            var app = provider.GetService<App>();
            await app!.Start();
        }

        private static void ConfigureServices(ServiceCollection serviceCollection, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            serviceCollection.AddDbContextFactory<ApplicationDbContext>(options
                => options.UseSqlServer(connectionString));
            serviceCollection.AddScoped<IDbContextWrapper<ApplicationDbContext>, DbContextWrapper<ApplicationDbContext>>();

            serviceCollection
                .AddLogging(configure => configure.AddConsole())
                .AddTransient<ICategoryRepository, CategoryRepository>()
                .AddTransient<ICustomerRepository, CustomerRepository>()
                .AddTransient<IPaymentRepository, PaymentRepository>()
                .AddTransient<IProductRepository, ProductRepository>()
                .AddTransient<IOrderRepository, OrderRepository>()
                .AddTransient<IShipperRepository, ShipperRepository>()
                .AddTransient<ISupplierRepository, SupplierRepository>()
                .AddTransient<ICategoryService, CategoryService>()
                .AddTransient<ICustomerService, CustomerService>()
                .AddTransient<IPaymentService, PaymentService>()
                .AddTransient<IProductService, ProductService>()
                .AddTransient<IOrderService, OrderService>()
                .AddTransient<IShipperService, ShipperService>()
                .AddTransient<ISupplierService, SupplierService>()
                .AddTransient<App>();
        }
    }
}