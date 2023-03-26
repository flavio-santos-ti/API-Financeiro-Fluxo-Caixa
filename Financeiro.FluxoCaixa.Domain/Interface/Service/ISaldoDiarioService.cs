using Financeiro.FluxoCaixa.Domain.Dtos.Result;
using Financeiro.FluxoCaixa.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Financeiro.FluxoCaixa.Domain.Interface.Service;

public interface ISaldoDiarioService
{
    Task<SaldoDiario> GetNoTrackingAsync(DbContext ctx, DateTime data, string tipo);
    Task<ResultCreateDto> SetRegistrarAsync(DbContext ctx, SaldoDiario dados);
    Task<SaldoDiario> GetSituacaoAsync();
    Task<decimal> GetSaldoAnteriorAsync(DbContext ctx, DateTime data);
}
