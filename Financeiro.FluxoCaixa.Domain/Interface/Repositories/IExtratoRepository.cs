using Financeiro.FluxoCaixa.Domain.Dtos.Extrato;
using Financeiro.FluxoCaixa.Domain.Dtos.Result;
using Financeiro.FluxoCaixa.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Financeiro.FluxoCaixa.Domain.Interface.Repositories;

public interface IExtratoRepository
{
    Task<ResultCreateDto> SetIncluirAsync(DbContext ctx, Extrato dados);
    Task<IEnumerable<ExtratoDto>> GetListarAsync(ExtratoFiltroDto filtro);
    Task<decimal> GetSomarAsync(ExtratoFiltroDto filtro);
    Task<decimal> GetSomarAsync(DbContext ctx, DateTime data);
}
