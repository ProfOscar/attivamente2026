using AttivaMente.Data;
using Microsoft.AspNetCore.Mvc;

namespace AttivaMente.Web.Controllers
{
    public class UtenteController: Controller
    {
        private readonly UtenteRepository _repo;

        public UtenteController(IConfiguration config)
        {
            string connStr = config.GetConnectionString("DefaultConnection")!;
            _repo = new UtenteRepository(connStr);
        }

        public ActionResult Index()
        {
            ViewBag.subTitle = "- Utenti";
            var utenti = _repo.GetAll();
            return View(utenti);
        }
    }
}
