using DIExampleContracts;
using DIExampleServices;

namespace DIExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();

            //builder.Services.Add(new ServiceDescriptor(
            //    typeof(ICitiesService),
            //    typeof(CitiesService),
            //    ServiceLifetime.Transient       // Creates a new instance every time it injected
            //    //ServiceLifetime.Scoped        // Creates one instance for each scope - request
            //    //ServiceLifetime.Singleton     // Creates an instance once and uses the same one entire application runtime
            //    ));

            builder.Services.AddTransient<ICitiesService, CitiesService>();     // Creates a new instance every time it injected
            //builder.Services.AddScoped<ICitiesService, CitiesService>();        // Creates one instance for each scope - request
            //builder.Services.AddSingleton<ICitiesService, CitiesService>();     // Creates an instance once and uses the same one entire application runtime

            var app = builder.Build();

            app.UseStaticFiles();
            app.UseRouting();
            app.MapControllers();

            app.Run();
        }
    }
}
