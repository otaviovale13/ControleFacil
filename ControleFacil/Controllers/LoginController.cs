using Microsoft.AspNetCore.Mvc;

namespace ControleFacil.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
