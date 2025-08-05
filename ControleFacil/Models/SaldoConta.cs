namespace ControleFacil.Models
{
    public class SaldoConta
    {
        public int ContaId { get; set; }
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public DateTime DataCriacao { get; set; }
        public decimal Valor { get; set; }
    }

}
