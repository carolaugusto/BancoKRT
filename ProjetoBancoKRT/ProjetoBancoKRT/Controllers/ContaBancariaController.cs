using Microsoft.AspNetCore.Mvc;
using ProjetoBancoKRT.Interface;
using ProjetoBancoKRT.Models;

public class ContaBancariaController : Controller
{
    private readonly IContaBancariaService _contaBancariaService;

    public ContaBancariaController(IContaBancariaService contaBancariaService)
    {
        _contaBancariaService = contaBancariaService;
    }

    [HttpGet]
    public IActionResult Cadastrar()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Cadastrar(ContaBancaria contaBancaria)
    {
        if (ModelState.IsValid)
        {
            var contaExistente = await _contaBancariaService.BuscarAsync(contaBancaria.Documento);
            if (contaExistente != null)
            {
                TempData["Message"] = "CPF já cadastrado.";
                return View(contaBancaria);
            }

            await _contaBancariaService.SalvarAsync(contaBancaria);
            TempData["Message"] = "Conta bancária cadastrada com sucesso!";
        }
        else
        {
            TempData["Message"] = "Dados inválidos.";
        }
        return View(contaBancaria);
    }

    [HttpGet]
    public IActionResult Buscar()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Buscar(string cpf)
    {
        if (string.IsNullOrEmpty(cpf))
        {
            TempData["Message"] = "O CPF não pode ser vazio.";
            return View();
        }

        var conta = await _contaBancariaService.BuscarAsync(cpf);
        if (conta == null)
        {
            TempData["Message"] = "Conta bancária não encontrada.";
            return View();
        }

        return View(conta);
    }

    [HttpPost]
    public async Task<IActionResult> Deletar(string cpf)
    {
        try
        {
            await _contaBancariaService.DeletarAsync(cpf);
            TempData["Message"] = "Conta bancária deletada com sucesso!";
        }
        catch (Exception ex)
        {
            TempData["Message"] = $"Erro ao deletar conta: {ex.Message}";
        }

        return RedirectToAction("Buscar");
    }

    [HttpPost]
    public async Task<IActionResult> Atualizar(string cpf, int novoLimite)
    {
        try
        {
            await _contaBancariaService.AtualizarLimiteAsync(cpf, novoLimite);
            TempData["Message"] = "Limite atualizado com sucesso.";
        }
        catch (Exception ex)
        {
            TempData["Message"] = $"Erro ao atualizar limite: {ex.Message}";
        }

        return RedirectToAction("Buscar", new { cpf });
    }

    [HttpGet]
    public IActionResult Transferir()
    {
        return View(new Transferir());
    }

    [HttpPost]
    public async Task<IActionResult> Transferir(Transferir model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            await _contaBancariaService.TransferirAsync(model.CpfOrigem, model.CpfDestino, model.Valor);
            TempData["Message"] = "Transferência realizada com sucesso.";
        }
        catch (ArgumentException ex)
        {
            TempData["Message"] = ex.Message;
        }
        catch (InvalidOperationException ex)
        {
            TempData["Message"] = ex.Message;
        }
        catch (Exception ex)
        {
            TempData["Message"] = $"Erro na transferência: {ex.Message}";
        }

        return RedirectToAction("Buscar");
    }
}
