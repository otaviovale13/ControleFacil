using ControleFacil.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleFacil.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<TokenRecuperacao> TokensRecuperacao { get; set; }
        public DbSet<Receita> Receitas { get; set; }
        public DbSet<Despesa> Despesas { get; set; }
        public DbSet<Conta> Contas { get; set; }
        public DbSet<Fatura> Faturas { get; set; }
        public DbSet<Parcelamento> Parcelamentos { get; set; }
        public DbSet<SaldoConta> SaldoContas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SaldoConta>().HasNoKey().ToView("SaldoContas");

            // Usuário
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Receita
            modelBuilder.Entity<Receita>()
                .HasIndex(r => new { r.UsuarioId, r.Nome })
                .IsUnique();

            // Despesa
            modelBuilder.Entity<Despesa>()
                .HasIndex(d => new { d.UsuarioId, d.Nome })
                .IsUnique();

            // Conta
            modelBuilder.Entity<Conta>()
                .HasIndex(c => new { c.UsuarioId, c.Nome })
                .IsUnique();

            // Relacionamentos explícitos (opcional)
            modelBuilder.Entity<TokenRecuperacao>()
                .HasOne(t => t.Usuario)
                .WithMany()
                .HasForeignKey(t => t.UsuarioId);

            modelBuilder.Entity<Receita>()
                .HasOne(r => r.Usuario)
                .WithMany(u => u.Receitas)
                .HasForeignKey(r => r.UsuarioId);

            modelBuilder.Entity<Despesa>()
                .HasOne(d => d.Usuario)
                .WithMany(u => u.Despesas)
                .HasForeignKey(d => d.UsuarioId);

            modelBuilder.Entity<Conta>()
                .HasOne(c => c.Usuario)
                .WithMany(u => u.Contas)
                .HasForeignKey(c => c.UsuarioId);

            modelBuilder.Entity<Fatura>()
                .HasOne(f => f.Conta)
                .WithMany(c => c.Faturas)
                .HasForeignKey(f => f.ContaId);

            modelBuilder.Entity<Parcelamento>()
                .HasOne(p => p.Usuario)
                .WithMany(u => u.Parcelamentos)
                .HasForeignKey(p => p.UsuarioId);

            modelBuilder.Entity<Despesa>()
                .HasOne(d => d.Parcelamento)
                .WithMany(p => p.Despesas)
                .HasForeignKey(d => d.ParcelamentoId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
