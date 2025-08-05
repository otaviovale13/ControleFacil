namespace ControleFacil.Models
{
    public class Fatura
    {
        public int Id { get; set; }
        public int ContaId { get; set; }
        public byte Estado { get; set; }
        public decimal Valor { get; set; }

        public Conta Conta { get; set; }
        public string Status { get; internal set; }
    }
}
