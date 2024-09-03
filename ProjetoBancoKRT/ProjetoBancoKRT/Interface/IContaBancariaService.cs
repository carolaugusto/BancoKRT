using ProjetoBancoKRT.Models;

namespace ProjetoBancoKRT.Interface
{
    public interface IContaBancariaService
    {
        Task<ContaBancaria> BuscarAsync(string cpf);
        Task SalvarAsync(ContaBancaria conta);
        Task DeletarAsync(string cpf);
        Task AtualizarLimiteAsync(string cpf, int novoLimite);
        Task TransferirAsync(string cpfOrigem, string cpfDestino, double valor);
    }
}
