﻿using AutoMapper;
using Financeiro.FluxoCaixa.Domain.Dtos.Fornecedor;
using Financeiro.FluxoCaixa.Domain.Dtos.Pessoa;
using Financeiro.FluxoCaixa.Domain.Dtos.Result;
using Financeiro.FluxoCaixa.Domain.Entities;
using Financeiro.FluxoCaixa.Domain.Interface.Repositories;
using Financeiro.FluxoCaixa.Domain.Interface.Service;
using Financeiro.FluxoCaixa.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

namespace Financeiro.FluxoCaixa.Business.Service;

public class FornecedorService : BaseService, IFornecedorService
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly IPessoaService _pessoaService;
    private readonly IFornecedorRepository _fornecedorRepository;

    public FornecedorService(IConfiguration configuration, IMapper mapper, IPessoaService pessoaService, IFornecedorRepository fornecedorRepository)
    {
        _configuration = configuration;
        _mapper = mapper;
        _pessoaService = pessoaService;
        _fornecedorRepository = fornecedorRepository;
    }

    private async Task<ResultCreateDto> SetIncluirAsync(DbContext ctx, long idPessoa)
    {
        var context = (DatabaseContext)ctx;

        try
        {
            Fornecedor fornecedorNew = new();
            fornecedorNew.PessoaId = idPessoa;
            fornecedorNew.DataInclusao = DateTime.Now;
            context.Fornecedores.Add(fornecedorNew);
            await context.SaveChangesAsync();

            return base.ResultCreate(true, "Fornecedor cadastrado com sucesso!", fornecedorNew.Id);
        }
        catch (Exception ex)
        {
            return base.ResultCreate(false, "Erro ao tentar cadastrar o fornecedor:" + ex.Message, 0);
        }
    }

    private async Task<Fornecedor> GetNoTrackingAsync(DbContext ctx, long idFornecedor)
    {
        var context = (DatabaseContext)ctx;
        
        return await _fornecedorRepository.GetNoTrackingAsync(context, idFornecedor);
    }

    public async Task<IEnumerable<FornecedorDto>> GetListarAsync()
    {
        return await _fornecedorRepository.GetListarAsync();
    }


    public async Task<ResultCreateDto> SetCadastrarAsync(FornecedorCreateDto dados)
    {
        using (var context = new DatabaseContext(_configuration))
        {
            using (IDbContextTransaction transaction = await context.Database.BeginTransactionAsync())
            {
                var pessoaCreate = _mapper.Map<PessoaCreateDto>(dados);

                var pessoa = await _pessoaService.SetIncluirAsync(context, pessoaCreate);

                if (pessoa.Successed)
                {
                    var fornecedorRegistered = await context.Fornecedores
                        .Where(b => b.PessoaId == pessoa.Id)
                        .FirstOrDefaultAsync();

                    if (fornecedorRegistered == null)
                    {
                        var fornecedorNew = await this.SetIncluirAsync(context, pessoa.Id);
                        if (fornecedorNew.Successed)
                        {
                            await transaction.CommitAsync();
                            return fornecedorNew;
                        }
                        else
                        {
                            await transaction.RollbackAsync();
                            return fornecedorNew;
                        }
                    }
                    else
                    {
                        await transaction.RollbackAsync();
                        return base.ResultCreate(false, "Fornecedor já cadastrado", fornecedorRegistered.Id);
                    }
                }
                else
                {
                    await transaction.RollbackAsync();
                    return base.ResultCreate(false, pessoa.Message, pessoa.Id);
                }
            }
        }

    }

    public async Task<ResultCreateDto> ParseAsync(DbContext ctx, long id)
    {
        var context = (DatabaseContext)ctx;

        string invalidMessage = string.Empty;

        var fornecedor = await GetNoTrackingAsync(context, id);

        if (fornecedor == null) 
            invalidMessage = "Fornecedor não cadastrado!";

        return base.ResultCreate((invalidMessage == string.Empty), invalidMessage, id);
    }
}
