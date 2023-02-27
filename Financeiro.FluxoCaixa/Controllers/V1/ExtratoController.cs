using Financeiro.FluxoCaixa.Domain.DTO.Extrato;
using Financeiro.FluxoCaixa.Domain.Interface.Service;
using Microsoft.AspNetCore.Mvc;

namespace Financeiro.FluxoCaixa.Controllers.V1;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ExtratoController : ControllerBase
{
    private readonly IExtratoService _service;

    public ExtratoController(IExtratoService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> GetListarAsync(ExtratoFiltroDTO filtro)
    {
        try
        {
            var extrato = await _service.GetListarAsync(filtro);
            return Ok(new { Successed = true, Count = extrato.Count(), Extrato = extrato });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Successed = false, Erros = ex.Message });
        }
    }

    [HttpPost("Saldo")]
    public async Task<IActionResult> GetSomarAsync(ExtratoFiltroDTO filtro)
    {
        try
        {
            decimal saldo = await _service.GetSomarAsync(filtro);
            return Ok(new { Successed = true,  Saldo = saldo });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Successed = false, Erros = ex.Message });
        }
    }

}
