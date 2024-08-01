using Microsoft.AspNetCore.Mvc;

namespace ExceptionHandling.Controllers
{
    public class ErrorController : Controller
    {
        // General error page
        [Route("Error/Index")]
        public IActionResult Index()
        {
            return View();
        }

        // 404 Not Found page
        [Route("Error/NotFound")]
        public IActionResult NotFound()
        {
            return View();
        }
    }
}
