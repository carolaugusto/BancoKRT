using Amazon.DynamoDBv2.DataModel;
using System.ComponentModel.DataAnnotations;

namespace ProjetoBancoKRT.Models
{

    [DynamoDBTable("ContasBancarias")]
    public class ContaBancaria
    {
        [Required]
        [DynamoDBHashKey] // Chave de Partição
        public string Documento { get; set; }

        [Required]
        [DynamoDBProperty]
        public int Agencia { get; set; }

        [Required]
        [DynamoDBProperty]
        public int Conta { get; set; }

        [Required]
        [Range(200, double.MaxValue, ErrorMessage = "O limite do Pix deve ser maior que 200.")]
        [DynamoDBProperty]
        public double Limite { get; set; }
    }
}

