using System;

namespace ControleFacil.Models
{
    public class Receita
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataCriacao { get; set; }
        public int ContaId { get; set; }

        public Conta Conta { get; set; }
        public Usuario Usuario { get; set; }
    }
}
