using System.Diagnostics;
using ControleFacil.Data;
using ControleFacil.Models;
using ControleFacil.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleFacil.Controllers
{
    public class HomeController : Controller
    {

        private readonly DBContext _context;

        public HomeController(DBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var user = _context.Usuarios.FirstOrDefault(u => u.Id == 1);
            if (user == null)
                return NotFound($"Usuário com ID 1 não encontrado.");

            ViewBag.Usuario = user; // Passa o usuário para a view e layout
            return View(user);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
