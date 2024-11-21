using System.Diagnostics.Metrics;

namespace CountryAssignment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Dictionary<int, string> countries = new Dictionary<int, string>
            {
                { 1, "United States" },
                { 2, "Canada" },
                { 3, "United Kingdom" },
                { 4, "India" },
                { 5, "Japan" }
            };

            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.Map("/", async context =>
                {
                    await context.Response.WriteAsync("Countries Assignment Root Path");
                });

                endpoints.Map("/countries", async context =>
                {
                    foreach (var country in countries)
                    {
                        await context.Response.WriteAsync($"{country.Key}, {country.Value}\n");
                    }
                });

                endpoints.Map("/countries/{countryId:int}", async context =>
                {
                    int countryId = Convert.ToInt32(context.Request.RouteValues["countryId"]);

                    if (countryId > 100)
                    {
                        context.Response.StatusCode = 400;
                        await context.Response.WriteAsync("The CountryID should be between 1 and 100");
                        return;
                    }

                    if (!countries.ContainsKey(countryId))
                    {
                        context.Response.StatusCode = 404;
                        await context.Response.WriteAsync($"[No Country]");
                        return;
                    }

                    context.Response.StatusCode = 200;
                    string country = countries[countryId];
                    await context.Response.WriteAsync($"{countryId}. {country}");
                });
            });

            //app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
