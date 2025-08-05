using System.Collections.Generic;
using static HomeController;

namespace ControleFacil.Models
{
    public class HomeViewModel
    {
        public decimal TotalReceitas { get; set; }
        public decimal TotalDespesas { get; set; }

        public decimal Saldo => TotalReceitas - TotalDespesas;

        public List<ContaViewModel> Contas { get; set; }

        public decimal FaturaAberta { get; set; }
    }
}
