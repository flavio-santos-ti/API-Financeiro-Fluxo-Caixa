using Financeiro.FluxoCaixa.Domain.Dtos.Result;
using Financeiro.FluxoCaixa.Domain.Dtos.SaldoDiario;
using Financeiro.FluxoCaixa.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Financeiro.FluxoCaixa.Domain.Interface.Repository;

public interface ISaldoDiarioRepository
{
    Task<ResultCreateDto> SetIncluirAsync(DbContext ctx, SaldoDiario dados);
    Task<IEnumerable<SaldoDiarioDto>> GetListarAsync(SaldoDiarioFiltroDto filtro);
    Task<SaldoDiario> GetNoTrackingAsync(DbContext ctx, DateTime data, string tipo);
    Task<SaldoDiario> GetSituacaoAsync();
    Task<SaldoDiario> GetAsync(DbContext ctx, DateTime data);
    Task<decimal> GetSaldoAnteriorAsync(DbContext ctx, DateTime data);
}
