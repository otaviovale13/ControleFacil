using Microsoft.AspNetCore.Mvc;

namespace ControleFacil.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cadastro()
        {
            return View();
        }

        public IActionResult Esqueceu()
        {
            return View();
        }

        public IActionResult Senha()
        {
            return View();
        }
    }
}
