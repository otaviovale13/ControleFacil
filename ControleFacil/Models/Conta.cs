using System;
using System.Collections.Generic;

namespace ControleFacil.Models
{
    public class Conta
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataCriacao { get; set; }

        public Usuario Usuario { get; set; }
        public ICollection<Fatura> Faturas { get; set; }
    }
}
