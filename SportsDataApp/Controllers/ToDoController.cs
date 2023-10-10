using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SportsDataApp.Controllers
{
    public class ToDoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
    }
}
