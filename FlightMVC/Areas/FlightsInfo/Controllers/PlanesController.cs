using FlightMVC.Areas.FlightsInfo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightMVC.Areas.FlightsInfo.Controllers
{
    [Area("FlightsInfo")]
    public class PlanesController : Controller
    {
        static List<Plane> _planes = new();

        static PlanesController()
        {
            _planes.Add(new Plane() { Id = 1, Model = "747", Colour = "Red, White and Blue", Capacity = 10 });
            _planes.Add(new Plane() { Id = 2, Model = "737", Colour = "White", Capacity = 180 });
            _planes.Add(new Plane() { Id = 3, Model = "777", Colour = "White", Capacity = 300 });
        }

        // GET: PlanesController
        public ActionResult Index()
        {
            return View(_planes);
        }

        // GET: PlanesController/Details/5
        public ActionResult Details(int id)
        {
            var p = _planes.FirstOrDefault(p => p.Id == id);

            if (p == null)
            {
                return NotFound();
            }

            return View(p);
        }

        // GET: PlanesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PlanesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PlanesController/Edit/5
        public ActionResult Edit(int id)
        {
            return Details(id);
        }

        // POST: PlanesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PlanesController/Delete/5
        public ActionResult Delete(int id)
        {
            return Details(id);
        }

        // POST: PlanesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
