using Microsoft.AspNetCore.Mvc;
using ControleFacil.Data;
using Microsoft.EntityFrameworkCore;
using ControleFacil.Models;

public class HomeController : Controller
{
    private readonly DBContext _context;

    public HomeController(DBContext context)
    {
        _context = context;
    }

    public class ContaViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
    }


    public async Task<IActionResult> Index()
    {
        int usuarioId = 1; // Pegue do login/sessão real

        var receitas = await _context.Receitas
            .Where(r => r.UsuarioId == usuarioId)
            .SumAsync(r => r.Valor);

        var despesas = await _context.Despesas
            .Where(d => d.UsuarioId == usuarioId)
            .SumAsync(d => d.Valor);

        var contas = await _context.SaldoContas
            .Where(c => c.UsuarioId == usuarioId)
            .Select(c => new ContaViewModel
            {
                Id = c.ContaId,
                Nome = c.Nome,
                Valor = c.Valor
            })
            .ToListAsync();

        var saldos = await _context.SaldoContas
            .Where(sc => sc.UsuarioId == usuarioId)
            .ToListAsync();

        var saldo = receitas - despesas;

        var fatura = await _context.Faturas
            .Where(f => f.Conta.UsuarioId == usuarioId && f.Estado == 1) // só abertas
            .SumAsync(f => f.Valor);

        var viewModel = new HomeViewModel
        {
            TotalReceitas = receitas,
            TotalDespesas = despesas,
            Contas = contas,
            FaturaAberta = fatura
        };

        return View(viewModel);
    }
}
