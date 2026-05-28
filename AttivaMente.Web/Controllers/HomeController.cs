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
            string connStr = _configuration.GetConnectionString("DefaultConnection")!;

            var utenteRepository = new UtenteRepository(connStr);
            var utenti = utenteRepository.GetAll();

            return View(utenti);
        }
    }
}
