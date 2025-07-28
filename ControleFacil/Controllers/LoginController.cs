using BCrypt.Net;
using ControleFacil.Data;
using ControleFacil.Models;
using ControleFacil.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControleFacil.Controllers
{
    public class LoginController : Controller
    {
        private readonly DBContext _context;

        private readonly EmailService _emailService;

        public LoginController(DBContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
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

        [HttpPost]
        public async Task<IActionResult> EnviarLinkRecuperacao(string email)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

            if (usuario == null)
            {
                TempData["Erro"] = "Email não encontrado.";
                return RedirectToAction("Esqueceu");
            }

            // Gerar token único
            var token = Guid.NewGuid().ToString();
            var tokenRecuperacao = new TokenRecuperacao
            {
                UsuarioId = usuario.Id,
                Token = token,
                ValidoAte = DateTime.UtcNow.AddHours(1)
            };

            _context.TokensRecuperacao.Add(tokenRecuperacao);
            await _context.SaveChangesAsync();

            // Criar link com token
            var link = Url.Action("Senha", "Login", new { token = token }, Request.Scheme);

            await _emailService.EnviarEmailAsync(
                usuario.Email,
                "Redefinir sua senha",
                $"Clique no link para redefinir sua senha: <a href=\"{link}\">{link}</a>"
            );

            TempData["Mensagem"] = "Um link foi enviado para o seu e-mail.";
            return RedirectToAction("Esqueceu");
        }

        [HttpGet]
        public async Task<IActionResult> Senha(string token)
        {
            var tokenValido = await _context.TokensRecuperacao
                .Include(t => t.Usuario)
                .FirstOrDefaultAsync(t => t.Token == token && t.ValidoAte > DateTime.UtcNow);

            if (tokenValido == null)
            {
                TempData["Erro"] = "Token inválido ou expirado.";
                return RedirectToAction("Index");
            }

            ViewData["Token"] = token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SalvarNovaSenha(string token, string novaSenha, string confirmarSenha)
        {
            if (novaSenha != confirmarSenha)
            {
                TempData["Erro"] = "As senhas não coincidem.";
                return RedirectToAction("Senha", new { token });
            }

            var tokenValido = await _context.TokensRecuperacao
                .Include(t => t.Usuario)
                .FirstOrDefaultAsync(t => t.Token == token && t.ValidoAte > DateTime.Now);

            if (tokenValido == null)
            {
                TempData["Erro"] = "Token inválido ou expirado.";
                return RedirectToAction("Esqueceu");
            }

            var usuario = tokenValido.Usuario;

            // ✅ Adicione os logs aqui:
            Console.WriteLine("ID do usuário: " + usuario.Id);
            Console.WriteLine("Nova senha (hash): " + BCrypt.Net.BCrypt.HashPassword(novaSenha));

            // Atualiza e salva a nova senha
            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(novaSenha);

            _context.Attach(usuario);
            _context.Entry(usuario).Property(u => u.Senha).IsModified = true;

            _context.TokensRecuperacao.Remove(tokenValido);
            await _context.SaveChangesAsync();

            TempData["Mensagem"] = "Senha redefinida com sucesso!";
            return RedirectToAction("Index");
        }
    }
}
