using FlightMVC.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FlightMVC.Controllers
{
    public class MyErrorController : Controller
    {
        [ServiceFilter(typeof(MyExceptionFilterAttribute))]
        public IActionResult Index()
        {
            throw new OverflowException();

            return View();
        }
    }
}
