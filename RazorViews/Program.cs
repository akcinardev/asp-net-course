namespace RazorViews
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            var app = builder.Build();

            app.MapControllers();
            app.UseStaticFiles();
            app.Run();
        }
    }
}
