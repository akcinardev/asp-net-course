using Microsoft.AspNetCore.Mvc;
using ControllerExample.Models;

namespace ControllerExample.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public ContentResult Index()
        {
            ContentResult result = new ContentResult
            {
                Content = "<h1>Index Result</h1>",
                ContentType = "text/html"
            };
            return result;
        }

        [Route("/about")]
        public string About()
        {
            return "About Method";
        }

        [Route("/contact/{mobile:regex(^\\d{{10}}$)}")]
        public string Contact()
        {
            return "Contact Method";
        }

        [Route("/person")]
        public JsonResult Person()
        {
            Person person = new Person()
            {
                Id = Guid.NewGuid(),
                FirstName = "Ömer",
                LastName = "Akçınar",
                Age = 26,
            };
            
            return Json(person);
            //return new JsonResult(person);
            //return "{\"key\": \"value\"}";
        }

        [Route("/virtual-file")]
        public VirtualFileResult VirtualFile() // Represents a file WITHIN WebRoot (wwwroot) folder
        {
            return File("/sample.pdf","application/pdf");
            //return new VirtualFileResult("/sample.pdf","application/pdf");
        }

        [Route("/physical-file")]
        public PhysicalFileResult PhysicalFile() // Represents a file OUTSIDE WebRoot (wwwroot) folder
        {
            return PhysicalFile("C:\\Users\\OMRAKC\\source\\repos\\files\\sample.pdf", "application/pdf");
            //return new PhysicalFileResult("C:\\Users\\OMRAKC\\source\\repos\\files\\sample.pdf", "application/pdf");
        }

        [Route("/content-file")]
        public FileContentResult FileContent() // Represents a byte file 
        {
            byte[] bytes = System.IO.File.ReadAllBytes("C:\\Users\\OMRAKC\\source\\repos\\files\\sample.pdf");
            return File(bytes, "application/pdf");
            //return new FileContentResult(bytes, "application/pdf");
        }
    }
}
