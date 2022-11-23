using FlightMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightMVC.Components
{
    public class QuestionAnswerViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string question, string answer)
        {
            var qa = new QuestionAnswer { Question = question, Answer = answer }; 
            return View(qa);
        }
    }
}
