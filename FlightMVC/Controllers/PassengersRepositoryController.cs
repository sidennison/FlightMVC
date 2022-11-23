using FlightMVC.Models;
using FlightMVC.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FlightMVC.Controllers
{
    //[Route("pd")]
    public class PassengersRepositoryController : Controller
    {
        private IPassengersRepository _repository;

        public PassengersRepositoryController(IPassengersRepository repository)
        {
            _repository = repository;
        }

        // GET: PassengersController
        //[HttpGet("index")]
        public ActionResult Index()
        {
            return View(_repository.GetPassengers());
        }

        // GET: PassengersController/Details/name
        public ActionResult Details(string id)
        {
            var pd = _repository.GetPassenger(id);

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
            if (!ModelState.IsValid)
            {
                return View(collection);
            }

            try
            {
                _repository.AddPassenger(collection);

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
                var pd = _repository.GetPassenger(id);

                if (pd == null)
                {
                    return NotFound();
                }

                // Save all passenger changes to the session
                var json = HttpContext.Session.GetString("ChangedPassengerDetails");
                var changes = json == null ? new List<PassengerDetails>() : JsonConvert.DeserializeObject<List<PassengerDetails>>(json);

                changes?.Add(pd);
                HttpContext.Session.SetString("ChangedPassengerDetails", JsonConvert.SerializeObject(changes));

                _repository.UpdatePassenger(collection);

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
                _repository.DeletePassenger(collection.Name);

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
