using FlightMVC.Data;
using FlightMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FlightMVC.Components
{
    public class FlightViewComponent : ViewComponent
    {
        ApplicationDbContext _ctx;

        public FlightViewComponent(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public IViewComponentResult Invoke(string flightNo)
        {
            var f = _ctx.Flights.FirstOrDefault(f => f.FlightNo == flightNo);
            return View(f);
        }
    }
}
