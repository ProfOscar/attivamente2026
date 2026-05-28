using AttivaMente.Data;
using Microsoft.AspNetCore.Mvc;

namespace AttivaMente.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            string connStr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Dati\\AttivaMenteDB.mdf;Integrated Security=True;Connect Timeout=30";

            var utenteRepository = new UtenteRepository(connStr);
            var utenti = utenteRepository.GetAll();

            return View(utenti);
        }
    }
}
