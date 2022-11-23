using FlightMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FlightMVC.Controllers
{
    //[Route("pd")]
    //[Authorize(Roles ="Admin")]
    [Authorize(Policy = "adminPolicy")]
    public class PassengersController : Controller
    {
        static readonly List<PassengerDetails> _passengers = new List<PassengerDetails>();

        static PassengersController()
        {
            _passengers.Add(new PassengerDetails("Fred", 23));
            _passengers.Add(new PassengerDetails("Graham", 15));
        }

        // GET: PassengersController
        //[HttpGet("index")]
        public ActionResult Index()
        {
            return View(_passengers.OrderBy(p => p.Name));
        }

        // GET: PassengersController/Details/name
        public ActionResult Details(string id)
        {
            var pd = _passengers.FirstOrDefault(p => p.Name == id);

            if (pd == null)
            {
                return NotFound();
            }

            return View(pd);
        }

        // GET: PassengersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PassengersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PassengerDetails collection)
        {
            if(_passengers.FirstOrDefault(p => p.Name == collection.Name) != null)
            {
                ModelState.AddModelError(String.Empty, "Name already exists");
            }

            if (!ModelState.IsValid)
            {
                return View(collection);
            }

            try
            {
                _passengers.Add(collection);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(collection);
            }
        }

        // GET: PassengersController/Edit/name
        public ActionResult Edit(string id)
        {
            return Details(id);
        }

        // POST: PassengersController/Edit/name
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, PassengerDetails collection)
        {
            if (!ModelState.IsValid)
            {
                return View(collection);
            }

            try
            {
                var pd = _passengers.FirstOrDefault(p => p.Name == id);

                if (pd == null)
                {
                    return NotFound();
                }

                // Save all passenger changes to the session
                var json = HttpContext.Session.GetString("ChangedPassengerDetails");
                var changes = json == null ? new List<PassengerDetails>() : JsonConvert.DeserializeObject<List<PassengerDetails>>(json);

                changes?.Add(pd);
                HttpContext.Session.SetString("ChangedPassengerDetails", JsonConvert.SerializeObject(changes));

                _passengers.Remove(pd);
                _passengers.Add(collection);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(collection);
            }
        }

        // GET: PassengersController/Delete/name
        public ActionResult Delete(string id)
        {
            return Details(id);
        }

        // POST: PassengersController/Delete/name
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, PassengerDetails collection)
        {
            try
            {
                var pd = _passengers.FirstOrDefault(p => p.Name == id);

                if (pd != null)
                {
                    _passengers.Remove(pd);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(collection);
            }
        }

        public ActionResult GetChanges()
        {
            var json = HttpContext.Session.GetString("ChangedPassengerDetails");
            var changes = json == null ? new List<PassengerDetails>() : JsonConvert.DeserializeObject<List<PassengerDetails>>(json);

            return View(changes);
        }
    }
}
