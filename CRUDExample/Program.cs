using CRUDEntities;
using CRUDServiceContracts;
using CRUDServices;
using Microsoft.EntityFrameworkCore;

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

            builder.Services.AddDbContext<PersonsDbContext>(options =>
            {
                //options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

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
