using AttivaMente.Data;
using Microsoft.AspNetCore.Mvc;

namespace AttivaMente.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            // ViewData["subTitle"] = "- Homepage";
            ViewBag.subTitle = "- Homepage";

            //return View();
            return Redirect("Utente");
        }

        public IActionResult Help()
        {
            ViewData["subTitle"] = "- Help";
            return View();
        }
    }
}
