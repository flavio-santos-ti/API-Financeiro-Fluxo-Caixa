using Financeiro.FluxoCaixa.Domain.DTO.Categoria;
using Financeiro.FluxoCaixa.Domain.Interface.Service;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace Financeiro.FluxoCaixa.Controllers.V1;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class CategoriaController : ControllerBase
{
    private readonly ICategoriaService _service;
    private readonly IValidator<CategoriaCreateDTO> _validatorCategoriaCreate;

    public CategoriaController(ICategoriaService service, IValidator<CategoriaCreateDTO> validatorClienteCreate)
    {
        _service = service;
        _validatorCategoriaCreate = validatorClienteCreate;
    }


    [HttpPost]
    public async Task<IActionResult> SetCadastrarAsync(CategoriaCreateDTO dados)
    {
        var validation = await _validatorCategoriaCreate.ValidateAsync(dados);

        if (!validation.IsValid)
        {
            return BadRequest(new { Successed = false, Erros = validation.Errors });
        }

        var categoria = await _service.SetCadastrarAsync(dados);

        if (categoria.Successed)
        {
            return Ok(categoria);
        }
        else
        {
            return BadRequest(categoria);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> SetRemoverAsync(int id)
    {
        var categoria = await _service.SetRemoverAsync(id);

        if (categoria.Successed)
        {
            return Ok(categoria);
        }
        else
        {
            return BadRequest(categoria);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetListarAsync()
    {
        try
        {
            var categoria = await _service.GetListarAsync();
            return Ok(new { Successed = true, Count = categoria.Count(), Categorias = categoria } );
        }
        catch (Exception ex)
        {
            return BadRequest(new { Successed = false, Erros = ex.Message });
        }
    }



}
