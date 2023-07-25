using FreeCourse.Web.Models;
using FreeCourse.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Web.Controllers
{
    public class AutController : Controller
    {
        private readonly IdentityService _identityService;

        public AutController(IdentityService identityService)
        {
            _identityService = identityService;
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SigninInput signinInput)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            var response = await _identityService.SignIn(signinInput);

            if(!response.IsSuccessful)
            {
                response.Errors.ForEach(error =>
                {
                    ModelState.AddModelError(string.Empty, error);

                });
                
                return View();
            }

            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
