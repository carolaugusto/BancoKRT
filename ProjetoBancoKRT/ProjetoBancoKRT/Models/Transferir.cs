using System.ComponentModel.DataAnnotations;

namespace ProjetoBancoKRT.Models
{
    public class Transferir
    {

        [Required]
        public string CpfOrigem { get; set; }

        [Required]
        public string CpfDestino { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
        public double Valor { get; set; }
    }
}

