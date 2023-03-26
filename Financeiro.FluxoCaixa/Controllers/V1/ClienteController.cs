using Financeiro.FluxoCaixa.Domain.Dtos;
using Financeiro.FluxoCaixa.Domain.Dtos.Cliente;
using Financeiro.FluxoCaixa.Domain.Interface.Service;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Financeiro.FluxoCaixa.Controllers.V1;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _service;
    private readonly IValidator<ClienteCreateDto> _validatorClienteCreate;
    public ClienteController(IClienteService clienteService, IValidator<ClienteCreateDto> validatorClienteCreate)
    {
        _service = clienteService;
        _validatorClienteCreate = validatorClienteCreate;
    }

    [HttpPost]
    public async Task<IActionResult> SetCadastrarAsync(ClienteCreateDto dados)
    {
        var validation = await _validatorClienteCreate.ValidateAsync(dados);

        if (!validation.IsValid)
        {
            return BadRequest( new { Successed = false, Erros = validation.Errors } );
        }

        var cliente = await _service.SetCadastrarAsync(dados);

        if (cliente.Successed)
        {
            return Ok(cliente);
        } 
        else
        {
            return BadRequest(cliente);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetListarAsync()
    {
        try
        {
            var clientes = await _service.GetListarAsync();
            return Ok(new { Successed = true, Count = clientes.Count(), Clientes = clientes });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Successed = false, Erros = ex.Message });
        }
    }


}
