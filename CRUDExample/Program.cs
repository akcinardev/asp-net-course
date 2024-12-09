using CRUDServiceContracts;
using CRUDServices;

namespace CRUDExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            // Add Services
            builder.Services.AddSingleton<ICountryService, CountryService>();
            builder.Services.AddSingleton<IPersonService, PersonService>();

            var app = builder.Build();

            if (builder.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.MapControllers();

            app.Run();
        }
    }
}
