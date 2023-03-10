using Financeiro.FluxoCaixa.Domain.DTO.Extrato;
using Financeiro.FluxoCaixa.Domain.DTO.Result;
using Financeiro.FluxoCaixa.Domain.DTO.TituloPagar;
using Financeiro.FluxoCaixa.Domain.DTO.TituloReceber;
using Financeiro.FluxoCaixa.Domain.Entity;
using Financeiro.FluxoCaixa.Domain.Interface.Repository;
using Financeiro.FluxoCaixa.Domain.Interface.Service;
using Financeiro.FluxoCaixa.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Business.Service;

public class ExtratoService : BaseService, IExtratoService
{
    private readonly IExtratoRepository _extratoRepository;
    private readonly ISaldoDiarioService _saldoDiarioService;

    public ExtratoService(IExtratoRepository extratoRepository, ISaldoDiarioService saldoDiarioService)
    {
        _extratoRepository = extratoRepository;
        _saldoDiarioService = saldoDiarioService;
    }

    public async Task<ResultCreateDTO> SetRegistrarDebito(DbContext ctx, TituloPagarCreateDTO dados)
    {

        decimal saldoAnterior = await _saldoDiarioService.GetSaldoAnteriorAsync(ctx, dados.DataReal);

        Extrato debitoNew = new();

        debitoNew.Descricao = dados.Descricao;
        debitoNew.Tipo = "D";
        debitoNew.DataExtrato = dados.DataReal;
        debitoNew.Valor = dados.ValorReal * -1;
        debitoNew.Saldo = saldoAnterior + debitoNew.Valor;
        debitoNew.ValorRelatorio = dados.ValorReal * -1;

        debitoNew.DataInclusao = DateTime.Now;

        return await _extratoRepository.SetIncluirAsync(ctx, debitoNew);
    }

    public async Task<ResultCreateDTO> SetRegistrarDebito(DbContext ctx, ExtratoCreateDTO dados)
    {
        Extrato debitoNew = new();

        debitoNew.Descricao = dados.Descricao;
        debitoNew.Tipo = "D";
        debitoNew.DataExtrato = dados.DataExtrato;
        debitoNew.Valor = 0;
        debitoNew.Saldo = dados.Valor;
        debitoNew.ValorRelatorio = dados.Valor;
        debitoNew.DataInclusao = DateTime.Now;

        return await _extratoRepository.SetIncluirAsync(ctx, debitoNew);
    }


    public async Task<ResultCreateDTO> SetRegistrarCredito(DbContext ctx, TituloReceberCreateDTO dados)
    {
        decimal saldoAnterior = await _saldoDiarioService.GetSaldoAnteriorAsync(ctx, dados.DataReal);

        Extrato debitoNew = new();

        debitoNew.Descricao = dados.Descricao;
        debitoNew.Tipo = "C";
        debitoNew.DataExtrato = dados.DataReal;
        debitoNew.Valor = dados.ValorReal;
        debitoNew.Saldo = saldoAnterior + debitoNew.Valor;
        debitoNew.ValorRelatorio = debitoNew.Valor;
        debitoNew.DataInclusao = DateTime.Now;

        return await _extratoRepository.SetIncluirAsync(ctx, debitoNew);
    }

    public async Task<ResultCreateDTO> SetRegistrarCredito(DbContext ctx, ExtratoCreateDTO dados)
    {
        Extrato debitoNew = new();

        debitoNew.Descricao = dados.Descricao;
        debitoNew.Tipo = "C";
        debitoNew.DataExtrato = dados.DataExtrato;
        debitoNew.Valor = 0;
        debitoNew.Saldo = dados.Valor;
        debitoNew.ValorRelatorio = dados.Valor;
        debitoNew.DataInclusao = DateTime.Now;

        return await _extratoRepository.SetIncluirAsync(ctx, debitoNew);
    }


    public async Task<IEnumerable<ExtratoDTO>> GetListarAsync(ExtratoFiltroDTO filtro)
    {
        return await _extratoRepository.GetListarAsync(filtro);
    }

    public async Task<decimal> GetSomarAsync(ExtratoFiltroDTO filtro)
    {
        return await _extratoRepository.GetSomarAsync(filtro);
    }

    public async Task<decimal> GetSomarAsync(DbContext ctx, DateTime data)
    {
        return await _extratoRepository.GetSomarAsync(ctx, data);
    }



}
