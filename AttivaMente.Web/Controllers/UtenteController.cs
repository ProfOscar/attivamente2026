using AttivaMente.Core.Models;
using AttivaMente.Core.Security;
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

        public IActionResult Index()
        {
            ViewBag.subTitle = "- Utenti";
            var utenti = _repo.GetAll();
            return View(utenti);
        }

        public IActionResult Details(int id) {
            ViewBag.subTitle = $"- Utente {id}";
            var utente = _repo.GetById(id);
            return View(utente);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Utente utente, string password, string confermaPassword)
        {
            if (password == confermaPassword && password.Length > 3)
            {
                utente.PasswordHash = PasswordHelper.HashPassword(password);
                if (ModelState.IsValid)
                {
                    _repo.Add(utente);
                    return RedirectToAction("Index");
                }
            }
            return View(utente);
        }
    }
}
