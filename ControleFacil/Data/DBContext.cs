using System.Collections.Generic;
using ControleFacil.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleFacil.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<TokenRecuperacao> TokensRecuperacao { get; set; }
    }
}