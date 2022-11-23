using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace FlightMVC.Pages.PagesDemo
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string? Message { get; set; }
        ILogger _logger;

        public IndexModel(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("IndexPage");
        }

        public void OnGet()
        {
            Message = "Greetings";
        }

        public void OnPost()
        {
            Debug.WriteLine(Message);
            _logger.LogInformation(Message);
        }
    }
}
