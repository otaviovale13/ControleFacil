using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace ControleFacil.Models
{
    [Table("USUARIOS")]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        public ICollection<Receita> Receitas { get; set; }
        public ICollection<Despesa> Despesas { get; set; }
        public ICollection<Conta> Contas { get; set; }
        public ICollection<Parcelamento> Parcelamentos { get; set; }
    }
}