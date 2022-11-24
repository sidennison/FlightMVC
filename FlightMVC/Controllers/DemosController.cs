using FlightMVC.Filters;
using FlightMVC.Models;
using MessagePack.Formatters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FlightMVC.Controllers
{
    [Route("Demo")]
    public class DemosController : Controller
    {
        [HttpGet("pass")]
        public IActionResult TagDemo()
        {
            return View(new PassengerDetails("Graham", 24, "ba1234"));
        }

        [HttpGet("sent")]
        public IActionResult SomeContent()
        {
            return Content("Some nonsense sentence...");
        }

        [HttpGet("add/{n1:int}/{n2:int}")]
        public IActionResult Add(int n1, int n2)
        {
            return Content($"{n1+n2}");
        }

        [HttpGet("Number/{n:int:range(0,100)}")]
        public IActionResult SmallNumber(int n)
        {
            return Content($"Small number: {n}");
        }

        [HttpGet("Number/{n:int:range(101, 1000000)}")]
        public IActionResult LargeNumber(int n)
        {
            return Content($"Large number: {n}");
        }

        [HttpGet("DataViewOrBag")]
        public IActionResult DataViewOrBag()
        {
            ViewData["FoxText"] = "The quick brown fox jumps over the lazy dog!";

            ViewBag.Question = "The meaning of life the universe and everything?";

            return View();
        }

        [HttpGet("Select")]
        public IActionResult Select()
        {
            var items = new List<SelectListItem>
            {
                new SelectListItem { Text = "One", Value = "1", Selected = true },
                new SelectListItem { Text = "Two", Value = "2" },
                new SelectListItem { Text = "Three", Value = "3" }
            };

            ViewData["SelectList"] = items;

            return View();
        }

        [HttpGet("Powers")]
        public IActionResult Powers()
        {
            var powers = new List<IntegerPowersVM>();
            for (int i = 1; i <= 10; i++)
            {
                powers.Add(new IntegerPowersVM(i));
            }

            return View(powers);
        }

        [HttpGet("Partial")]
        public IActionResult PartialDemo()
        {
            // Cast to avoid overload for view name
            return View((object)"Short sentence.");
            //OR: return View("PartialDemo", "Short sentence.");
        }

        [HttpGet("QA")]
        public IActionResult QuestionAnswerDemo()
        {
            var qa = new QuestionAnswer() { Question = "Meaning of life, etc.?", Answer = "42" };
            return View(qa);
        }

        [NonAction]
        public string Message()
        {
            return "Hi";
        }

        [HttpGet("Filtering")]
        [ServiceFilter(typeof(MyActionFilterAttribute))]
        public IActionResult Filtering([FromServices]ILoggerFactory loggerFactory)
        {
            var logger = loggerFactory.CreateLogger("Action");
            logger.LogInformation("Filtering");
            return View();
        }

        [HttpGet("FlightService")]
        public IActionResult FlightService()
        {
            return View();
        }
    }
}
