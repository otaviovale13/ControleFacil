using System;

namespace ControleFacil.Models
{
    public class Despesa
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataCriacao { get; set; }
        public int? ParcelamentoId { get; set; }
        public int ContaId { get; set; }

        public Conta Conta { get; set; }
        public Parcelamento Parcelamento { get; set; }
        public Usuario Usuario { get; set; }
    }
}
