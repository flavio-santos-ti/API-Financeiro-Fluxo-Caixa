﻿using AutoMapper;
using Financeiro.FluxoCaixa.Domain.Dtos.Result;
using Financeiro.FluxoCaixa.Domain.Dtos.TituloPagar;
using Financeiro.FluxoCaixa.Domain.Entities;
using Financeiro.FluxoCaixa.Domain.Interface.Repositories;
using Financeiro.FluxoCaixa.Domain.Interface.Service;
using Financeiro.FluxoCaixa.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

namespace Financeiro.FluxoCaixa.Business.Service;

public class TituloPagarService : BaseService, ITituloPagarService
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly IFornecedorService _fornecedorService;
    private readonly ICategoriaService _categoriaService;
    private readonly IExtratoService _extratoService;
    private readonly ITituloPagarRepository _tituloPagarRepository;
    private readonly ICaixaService _caixaService;
    private readonly ISaldoDiarioService _saldoDiarioService;

    public TituloPagarService(IConfiguration configuration, IMapper mapper, IFornecedorService fornecedorService, ICategoriaService categoriaService, ITituloPagarRepository tituloPagarRepository, IExtratoService extratoService, ICaixaService caixaService, ISaldoDiarioService saldoDiarioService)
    {
        _configuration = configuration;
        _mapper = mapper;
        _fornecedorService = fornecedorService;
        _categoriaService = categoriaService;
        _tituloPagarRepository = tituloPagarRepository;
        _extratoService = extratoService;
        _caixaService = caixaService;
        _saldoDiarioService = saldoDiarioService;
    }

    private async Task<ResultCreateDto> ParseAsync(DbContext ctx, TituloPagarCreateDto dados)
    {
        var context = (DatabaseContext)ctx;

        var situacaoCaixa = await _caixaService.ParseLancamentoAsync(context, dados.DataReal);
        
        if (!situacaoCaixa.Successed) return situacaoCaixa;

        var situacaoFornecedor = await _fornecedorService.ParseAsync(context, dados.FornecedorId);

        if (!situacaoFornecedor.Successed) return situacaoFornecedor;

        var situacaoCategoria = await _categoriaService.ParseAsync(context, dados.CategoriaId);

        if (!situacaoCategoria.Successed) return situacaoCategoria;

        return base.ResultCreate(true, string.Empty, 0);
    }

    public async Task<ResultCreateDto> SetPagarAsync(TituloPagarCreateDto dados)
    {
        using (var context = new DatabaseContext(_configuration))
        {
            using (IDbContextTransaction transaction = await context.Database.BeginTransactionAsync())
            {
                dados.Descricao = ClearString(dados.Descricao.ToUpper());
                dados.DataReal = dados.DataReal.Date;
                
                var situacao = await ParseAsync(context, dados);

                if (!situacao.Successed)
                {
                    return situacao;
                }

                 
                var extrato = await _extratoService.SetRegistrarDebito(context, dados);
                
                if (!extrato.Successed) 
                    return extrato;

                decimal saldoAnterior = await _saldoDiarioService.GetSaldoAnteriorAsync(context, dados.DataReal);

                SaldoDiario saldoMovimento = new();
                saldoMovimento.Tipo = "M";
                saldoMovimento.DataSaldo = dados.DataReal;
                saldoMovimento.Valor = saldoAnterior + (dados.ValorReal * -1); 
                saldoMovimento.ExtratoId = extrato.Id;

                var saldo = await _saldoDiarioService.SetRegistrarAsync(context, saldoMovimento);

                if (!saldo.Successed)
                {
                    await transaction.RollbackAsync();
                    return saldo;
                }

                var titulo = _mapper.Map<TituloPagar>(dados);

                titulo.ExtratoId = extrato.Id;

                var tituloNew = await _tituloPagarRepository.SetPagarAsync(context, titulo);

                if (!tituloNew.Successed)
                {
                    await transaction.RollbackAsync();
                    return tituloNew;
                }
 
                await transaction.CommitAsync();
                return tituloNew;
            }

        }
    }

    public async Task<IEnumerable<TituloPagarDto>> GetListarAsync()
    {
        return await _tituloPagarRepository.GetListarAsync();
    }

}
