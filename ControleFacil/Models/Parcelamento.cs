using System;
using System.Collections.Generic;

namespace ControleFacil.Models
{
    public class Parcelamento
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public decimal ValorParcela { get; set; }
        public int TotalParcelas { get; set; }
        public DateTime DataInicio { get; set; }

        public Usuario Usuario { get; set; }
        public ICollection<Despesa> Despesas { get; set; }
    }
}
