using Financeiro.FluxoCaixa.Domain.Dtos.Result;
using Financeiro.FluxoCaixa.Domain.Entities;
using Financeiro.FluxoCaixa.Domain.Interface.Repositories;
using Financeiro.FluxoCaixa.Domain.Interface.Service;
using Microsoft.EntityFrameworkCore;

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

    public async Task<ResultCreateDto> SetRegistrarAsync(DbContext ctx, SaldoDiario dados)
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
