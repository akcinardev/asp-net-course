using Microsoft.AspNetCore.Mvc;

namespace BankAppAssignment.Controllers
{
    public class AccountController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return Content("<h1>Welcome to the Best Bank.</h1>", "text/html");
        }

        [Route("/account-details")]
        public IActionResult Details()
        {
            // Mock details of account
            var details = new
            {
                accountNumber = 1001,
                accountHolderName = "Omer Akcinar",
                currentBalance = 5000
            };

            return Json(details);
        }

        [Route("/account-statement")]
        public IActionResult Statement()
        {
            return File("sample.pdf", "application/pdf");
        }

        [Route("/get-current-balance/{accountNumber?}")]
        public IActionResult Balance()
        {
            if (!Request.RouteValues.ContainsKey("accountNumber") || Request.RouteValues["accountNumber"] == null)
            {
                return BadRequest("Account Number should be supplied");
            }

            // Mock details of account
            var details = new
            {
                accountNumber = 1001,
                accountHolderName = "Omer Akcinar",
                currentBalance = 5000
            };

            try
            {
                int accountNumber = Convert.ToInt32(Request.RouteValues["accountNumber"]);
                if (accountNumber != 1001)
                {
                    return BadRequest("No account has found with specified number.");
                }

                return Content($"<h3>Balance of the account number {accountNumber}: <strong>{details.currentBalance}</strong></h3>", "text/html");
            }
            catch
            {
                return BadRequest("Account number should be a valid number.");
            }
        }
    }
}
