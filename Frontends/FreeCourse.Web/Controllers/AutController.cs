using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Web.Controllers
{
    public class AutController : Controller
    {
        public IActionResult SignIn()
        {
            return View();
        }
    }
}
