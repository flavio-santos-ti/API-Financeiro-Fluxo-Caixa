using Financeiro.FluxoCaixa.Domain.Dtos.Fornecedor;
using Financeiro.FluxoCaixa.Domain.Interface.Service;
using Microsoft.AspNetCore.Mvc;

namespace Financeiro.FluxoCaixa.Controllers.V1;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class FornecedorController : ControllerBase
{
    private readonly IFornecedorService _service;

    public FornecedorController(IFornecedorService service)
    {
        this._service = service;
    }

    [HttpPost]
    public async Task<IActionResult> SetCadastrarAsync(FornecedorCreateDto dados)
    {
        var fornecedor = await _service.SetCadastrarAsync(dados);

        if (fornecedor.Successed)
        {
            return Ok(fornecedor);
        }
        else
        {
            return BadRequest(fornecedor);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetListarAsync()
    {
        try
        {
            var fornecedor = await _service.GetListarAsync();
            return Ok(new { Successed = true, Count = fornecedor.Count(), Fornecedores = fornecedor });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Successed = false, Erros = ex.Message });
        }
    }
}
