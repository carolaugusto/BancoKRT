using Amazon.DynamoDBv2.DataModel;
using ProjetoBancoKRT.Interface;
using ProjetoBancoKRT.Models;

public class ContaBancariaService : IContaBancariaService
{
    private readonly IDynamoDBContext _context;

    public ContaBancariaService(IDynamoDBContext context)
    {
        _context = context;
    }

    public async Task<ContaBancaria> BuscarAsync(string cpf)
    {
        return await _context.LoadAsync<ContaBancaria>(cpf);
    }

    public async Task SalvarAsync(ContaBancaria conta)
    {
        await _context.SaveAsync(conta);
    }

    public async Task DeletarAsync(string cpf)
    {
        var conta = new ContaBancaria { Documento = cpf };
        await _context.DeleteAsync(conta);
    }

    public async Task AtualizarLimiteAsync(string cpf, int novoLimite)
    {
        var conta = await BuscarAsync(cpf);
        if (conta != null)
        {
            conta.Limite = novoLimite;
            await SalvarAsync(conta);
        }
    }

    public async Task TransferirAsync(string cpfOrigem, string cpfDestino, double valor)
    {
        var contaOrigem = await BuscarAsync(cpfOrigem);
        var contaDestino = await BuscarAsync(cpfDestino);

        if (contaOrigem == null)
        {
            throw new ArgumentException("Conta de origem não encontrada.");
        }

        if (contaDestino == null)
        {
            throw new ArgumentException("Conta de destino não encontrada.");
        }

        if (valor <= 0)
        {
            throw new ArgumentException("O valor da transferência deve ser maior que zero.");
        }

        if (contaOrigem.Limite < valor)
        {
            throw new InvalidOperationException("Saldo insuficiente para a transferência.");
        }

        contaOrigem.Limite -= valor;

        await SalvarAsync(contaOrigem);
    }
}
