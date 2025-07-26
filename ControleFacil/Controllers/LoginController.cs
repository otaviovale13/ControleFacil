using ControleFacil.Data;
using ControleFacil.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace ControleFacil.Controllers
{
    public class LoginController : Controller
    {
        private readonly DBContext _context;

        public LoginController(DBContext context)
        {
            _context = context;
        }

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

        public class UsuarioCreateModel
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            public string Email { get; set; }
            public string Senha { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarUsuario([FromBody] UsuarioCreateModel model)
        {
            var existeUsuario = await _context.Usuarios.AnyAsync(u => u.Email == model.Email);
            if (existeUsuario)
            {
                return Conflict("Já existe um usário com esse email.");
            }

            var novoUsuario = new Usuario
            {
                Nome = model.Nome,
                Email = model.Email,
                Senha = BCrypt.Net.BCrypt.HashPassword(model.Senha)
            };

            _context.Usuarios.Add(novoUsuario);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                id = novoUsuario.Id,
                nome = novoUsuario.Nome,
                email = novoUsuario.Email
            });
        }

        public class LoginModel
        {
            public string Email { get; set; }
            public string Senha { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> LogarUsuario([FromBody] LoginModel model)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == model.Email);

            if (usuario == null)
            {
                return Conflict("Email não encontrado.");
            }

            bool senhaCorreta = BCrypt.Net.BCrypt.Verify(model.Senha, usuario.Senha);

            if (!senhaCorreta)
            {
                return Conflict("Senha incorreta.");
            }

            return Ok(new
            {
                id = usuario.Id,
                nome = usuario.Nome,
                email = usuario.Email
            });
        }
    }
}
