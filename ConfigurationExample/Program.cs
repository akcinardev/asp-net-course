namespace ConfigurationExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.Configure<ExampleApiOptions>(builder.Configuration.GetSection("exampleapi")); // Add Config Section as Model

            // Add Custom json config file //OLD
            //builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
            //{
            //    config.AddJsonFile("config.json", optional: true, reloadOnChange: true);
            //});

            // Add Custom json config file // Preferred
            builder.Configuration.AddJsonFile("config.json", optional: true, reloadOnChange: true);

            var app = builder.Build();

            app.UseStaticFiles();
            app.UseRouting();
            app.MapControllers();
            app.Run();
        }
    }
}
