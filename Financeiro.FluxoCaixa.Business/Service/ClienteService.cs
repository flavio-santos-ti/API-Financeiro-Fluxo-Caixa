using AutoMapper;
using Financeiro.FluxoCaixa.Domain.DTO.Cliente;
using Financeiro.FluxoCaixa.Domain.DTO.Pessoa;
using Financeiro.FluxoCaixa.Domain.DTO.Result;
using Financeiro.FluxoCaixa.Domain.Entity;
using Financeiro.FluxoCaixa.Domain.Interface.Repository;
using Financeiro.FluxoCaixa.Domain.Interface.Service;
using Financeiro.FluxoCaixa.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Business.Service;

public class ClienteService : BaseService, IClienteService
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly IPessoaService _pessoaService;
    private readonly IClienteRepository _clienteRepository;

    public ClienteService(IConfiguration configuration, IMapper mapper, IPessoaService pessoaService, IClienteRepository clienteRepository)
    {
        _configuration = configuration;
        _mapper = mapper;
        _pessoaService = pessoaService;
        _clienteRepository = clienteRepository;
    }

    private async Task<Cliente> GetNoTrackingAsync(DbContext ctx, long idCliente)
    {
        return await _clienteRepository.GetNoTrackingAsync(ctx, idCliente);
    }

    public async Task<ResultCreateDTO> SetCadastrarAsync(ClienteCreateDTO dados)
    {
        using (var context = new DatabaseContext(_configuration))
        {
            using (IDbContextTransaction transaction = await context.Database.BeginTransactionAsync())
            {
                var pessoaCreate = _mapper.Map<PessoaCreateDTO>(dados);

                var pessoa = await _pessoaService.SetIncluirAsync(context, pessoaCreate);

                if (pessoa.Successed)
                {
                    var clienteRegistered = await _clienteRepository.GetAsync(context, pessoa.Id);
                    
                    if (clienteRegistered == null)
                    {
                        var clienteNew = await _clienteRepository.SetIncluirAsync(context, pessoa.Id);
                        if (clienteNew.Successed)
                        {
                            await transaction.CommitAsync();
                            return clienteNew;
                        } else
                        {
                            await transaction.RollbackAsync();
                            return clienteNew;
                        }
                    }
                    else
                    {
                        await transaction.RollbackAsync();
                        return base.ResultCreate(false, "Cliente já cadastrado", clienteRegistered.Id);
                    }
                } else
                {
                    await transaction.RollbackAsync();
                    return base.ResultCreate(false, pessoa.Message, pessoa.Id);
                }
            }
        }
    }

    public async Task<IEnumerable<ClienteDTO>> GetListarAsync()
    {
        return await _clienteRepository.GetListarAsync();
    }

    public  async Task<ResultCreateDTO> ParseAsync(DbContext ctx, long id)
    {
        var context = (DatabaseContext)ctx;

        string invalidMessage = string.Empty;

        var categoria = await GetNoTrackingAsync(context, id);

        if (categoria == null) 
            invalidMessage = "Cliente não cadastrado!";

        return base.ResultCreate((invalidMessage == string.Empty), invalidMessage, id);
    }

}
