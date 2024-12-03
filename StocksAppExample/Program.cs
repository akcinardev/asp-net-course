using Microsoft.Extensions.DependencyInjection;
using StocksAppExample.Options;
using StocksAppExample.Services;

namespace StocksAppExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpClient();
            //builder.Services.Configure<TradingOptions>(options => options.DefaultStockSymbol = builder.Configuration.GetValue<string>("DefaultStockSymbol"));
            builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TradingOptions"));
            builder.Services.AddScoped<FinnhubService>();

            var app = builder.Build();
            
            // Services
            app.UseStaticFiles();
            app.UseRouting();
            app.MapControllers();
            app.Run();
        }
    }
}
