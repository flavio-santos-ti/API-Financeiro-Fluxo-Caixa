using Financeiro.FluxoCaixa.Domain.DTO.TituloPagar;
using Financeiro.FluxoCaixa.Domain.DTO.TituloReceber;
using Financeiro.FluxoCaixa.Domain.Interface.Service;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Financeiro.FluxoCaixa.Controllers.V1;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class TituloReceberController : ControllerBase
{
    private readonly ITituloReceberService _service;
    private readonly IValidator<TituloReceberCreateDTO> _validatorTituloCreate;

    public TituloReceberController(ITituloReceberService service, IValidator<TituloReceberCreateDTO> validatorTituloCreate)
    {
        _service = service;
        _validatorTituloCreate = validatorTituloCreate;
    }

    [HttpPost]
    public async Task<IActionResult> SetCadastrarAsync(TituloReceberCreateDTO dados)
    {
        var validation = await _validatorTituloCreate.ValidateAsync(dados);

        if (!validation.IsValid)
        {
            return BadRequest(new { Successed = false, Erros = validation.Errors.FirstOrDefault() });
        }

        var titulo = await _service.SetReceberAsync(dados);

        if (titulo.Successed)
        {
            return Ok(titulo);
        }
        else
        {
            return BadRequest(titulo);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetListarAsync()
    {
        try
        {
            var titulosReceber = await _service.GetListarAsync();
            return Ok(new { Successed = true, Count = titulosReceber.Count(), TitulosPagar = titulosReceber });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Successed = false, Erros = ex.Message });
        }
    }


}
