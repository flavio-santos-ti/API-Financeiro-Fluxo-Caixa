using Financeiro.FluxoCaixa.Domain.Dtos.TituloPagar;
using Financeiro.FluxoCaixa.Domain.Interface.Service;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Financeiro.FluxoCaixa.Controllers.V1;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class TituloPagarController : ControllerBase
{
    private readonly ITituloPagarService _service;
    private readonly IValidator<TituloPagarCreateDto> _validatorTituloCreate;

    public TituloPagarController(ITituloPagarService service, IValidator<TituloPagarCreateDto> validatorTituloCreate)
    {
        _service = service;
        _validatorTituloCreate = validatorTituloCreate;
    }

    [HttpPost]
    public async Task<IActionResult> SetCadastrarAsync(TituloPagarCreateDto dados)
    {
        var validation = await _validatorTituloCreate.ValidateAsync(dados);

        if (!validation.IsValid)
        {
            return BadRequest(new { Successed = false, Erros = validation.Errors.FirstOrDefault() });
        }

        string x = dados.DataReal.Month.ToString();

        var titulo = await _service.SetPagarAsync(dados);

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
            var titulosPagar = await _service.GetListarAsync();
            return Ok(new { Successed = true, Count = titulosPagar.Count(), TitulosPagar = titulosPagar });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Successed = false, Erros = ex.Message });
        }
    }

}
