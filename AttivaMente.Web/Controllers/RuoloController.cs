using AttivaMente.Core.Models;
using AttivaMente.Data;
using Microsoft.AspNetCore.Mvc;

namespace AttivaMente.Web.Controllers
{
    public class RuoloController: Controller
    {
        private readonly RuoloRepository _repo;

        public RuoloController(IConfiguration config)
        {
            string connStr = config.GetConnectionString("DefaultConnection")!;
            _repo = new RuoloRepository(connStr);
        }

        public ActionResult Index() {
            ViewBag.subTitle = "- Ruoli";
            var ruoli = _repo.GetAll();
            return View(ruoli);
        }

        public ActionResult Create() => View();

        [HttpPost]
        public ActionResult Create(Ruolo ruolo)
        {
            if (ModelState.IsValid)
            {
                _repo.Add(ruolo);
                return RedirectToAction("Index");
            }
            else
            {
                return View(ruolo);
            }
        }
    }
}
