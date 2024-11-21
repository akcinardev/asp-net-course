namespace ControllerExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers(); // adds all controller classes

            var app = builder.Build();

            app.MapControllers(); // adds routes and maps controllers methods

            app.Run();
        }
    }
}
