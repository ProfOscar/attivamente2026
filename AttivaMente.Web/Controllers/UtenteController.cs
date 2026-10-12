using AttivaMente.Core.Models;
using AttivaMente.Core.Security;
using AttivaMente.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AttivaMente.Web.Controllers
{
    public class UtenteController : Controller
    {
        private readonly UtenteRepository _repoUtenti;
        private readonly RuoloRepository _repoRuoli;

        private readonly SelectList? _slRuoli;

        public UtenteController(IConfiguration config)
        {
            string connStr = config.GetConnectionString("DefaultConnection")!;
            _repoUtenti = new UtenteRepository(connStr);
            _repoRuoli = new RuoloRepository(connStr);
            _slRuoli = new SelectList(_repoRuoli.GetAll(), "Id", "Nome");
        }

        public IActionResult Index()
        {
            ViewBag.subTitle = "- Utenti";
            var utenti = _repoUtenti.GetAll();
            return View(utenti);
        }

        public IActionResult Details(int id)
        {
            ViewBag.subTitle = $"- Utente {id}";
            var utente = _repoUtenti.GetById(id);
            return View(utente);
        }

        public IActionResult Create()
        {
            // Propongo la form in GET popolando il ViewBag per la combo dei ruoli
            ViewBag.slRuoli = _slRuoli;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Utente utente, string password, string confermaPassword)
        {
            if (password != null && confermaPassword != null && password.Length > 3 && password == confermaPassword)
            {
                utente.PasswordHash = PasswordHelper.HashPassword(password);
                if (ModelState.IsValid)
                {
                    _repoUtenti.Add(utente);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError("Password", "Password non valida o le password non coincidono");
            }
            // Se qualcosa va storto ripropongo la form in GET ripopolando il ViewBag per la combo dei ruoli
            ViewBag.slRuoli = _slRuoli;
            return View(utente);
        }

        public IActionResult Delete(int id) {
            var utente = _repoUtenti.GetById(id);
            if (utente == null) return NotFound();
            return View(utente);
        }
        
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            _repoUtenti.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var utente = _repoUtenti.GetById(id);
            if (utente == null) return NotFound();
            ViewBag.slRuoli = _slRuoli;
            return View(utente);
        }

        [HttpPost]
        public IActionResult Edit(Utente utente)
        {
            if (ModelState.IsValid)
            {
                var oldUtente = _repoUtenti.GetById(utente.Id);
                if (oldUtente == null) return NotFound();
                utente.PasswordHash = oldUtente.PasswordHash;
                _repoUtenti.Update(utente, utente.Id);
                return RedirectToAction("Index");
            }
            ViewBag.slRuoli = _slRuoli;
            return View(utente);
        }
    }
}
