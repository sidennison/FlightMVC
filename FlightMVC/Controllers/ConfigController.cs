using FlightMVC.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FlightMVC.Controllers
{
    public class ConfigController : Controller
    {
        private readonly ConfigData _data;

        public ConfigController(IOptions<ConfigData> options)
        {
            _data = options.Value;
        }

        public IActionResult Index()
        {
            return View(_data);
        }
    }
}
