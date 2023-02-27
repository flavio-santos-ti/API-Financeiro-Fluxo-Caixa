using Financeiro.FluxoCaixa.Domain.DTO.Result;
using Financeiro.FluxoCaixa.Domain.Entity;
using Financeiro.FluxoCaixa.Domain.Interface.Repository;
using Financeiro.FluxoCaixa.Domain.Interface.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Business.Service;

public class SaldoDiarioService : BaseService, ISaldoDiarioService
{
    private readonly ISaldoDiarioRepository _saldoDiarioRepository;

    public SaldoDiarioService(ISaldoDiarioRepository saldoDiarioRepository)
    {
        _saldoDiarioRepository = saldoDiarioRepository;
    }

    public async Task<SaldoDiario> GetNoTrackingAsync(DbContext ctx, DateTime data, string tipo)
    {
        return await _saldoDiarioRepository.GetNoTrackingAsync(ctx, data, tipo);        
    }

    public async Task<ResultCreateDTO> SetRegistrarAsync(DbContext ctx, SaldoDiario dados)
    {
        return await _saldoDiarioRepository.SetIncluirAsync(ctx, dados);
    }

    public async Task<SaldoDiario> GetSituacaoAsync()
    {
        return await _saldoDiarioRepository.GetSituacaoAsync();
    }

    public async Task<decimal> GetSaldoAnteriorAsync(DbContext ctx, DateTime data)
    {
        return await _saldoDiarioRepository.GetSaldoAnteriorAsync(ctx, data);
    }

}
