using Financeiro.FluxoCaixa.Domain.DTO.Caixa;
using Financeiro.FluxoCaixa.Domain.DTO.Cliente;
using Financeiro.FluxoCaixa.Domain.Interface.Service;
using Microsoft.AspNetCore.Mvc;

namespace Financeiro.FluxoCaixa.Controllers.V1;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class CaixaController : ControllerBase
{
    private readonly ICaixaService _service;

    public CaixaController(ICaixaService service)
    {
        _service = service;
    }

    [HttpPost("Abrir")]
    public async Task<IActionResult> SetAbrirAsync(CaixaCreateDTO dados)
    {
        var caixa = await _service.SetAbrirAsync(dados);

        if (caixa.Successed)
        {
            return Ok(caixa);
        }
        else
        {
            return BadRequest(caixa);
        }
    }

    [HttpPost("Fechar")]
    public async Task<IActionResult> SetFecharAsync(CaixaCreateDTO dados)
    {
        var caixa = await _service.SetFecharAsync(dados);

        if (caixa.Successed)
        {
            return Ok(caixa);
        }
        else
        {
            return BadRequest(caixa);
        }
    }

    [HttpGet("Situacao")]
    public async Task<IActionResult> GetSituacaoAsync()
    {
        var caixa = await _service.GetSituacaoAsync();
        
        return Ok(caixa);
    }


}
