namespace ControleFacil.Models
{
    public class TokenRecuperacao
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Token { get; set; }
        public DateTime ValidoAte { get; set; }

        public Usuario Usuario { get; set; }
    }
}
