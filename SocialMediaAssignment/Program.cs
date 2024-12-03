using SocialMediaAssignment.Options;

namespace SocialMediaAssignment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.Configure<SocialMediaLinkOptions>(builder.Configuration.GetSection("SocialMediaLinks"));
            var app = builder.Build();

            app.MapControllers();

            app.Run();
        }
    }
}
