using Financeiro.FluxoCaixa.Domain.Dtos.Extrato;
using Financeiro.FluxoCaixa.Domain.Dtos.Result;
using Financeiro.FluxoCaixa.Domain.Dtos.TituloPagar;
using Financeiro.FluxoCaixa.Domain.Dtos.TituloReceber;
using Microsoft.EntityFrameworkCore;

namespace Financeiro.FluxoCaixa.Domain.Interface.Service;

public interface IExtratoService
{
    Task<ResultCreateDto> SetRegistrarDebito(DbContext ctx, TituloPagarCreateDto dados);
    Task<ResultCreateDto> SetRegistrarCredito(DbContext ctx, TituloReceberCreateDto dados);
    Task<IEnumerable<ExtratoDto>> GetListarAsync(ExtratoFiltroDto filtro);
    Task<decimal> GetSomarAsync(ExtratoFiltroDto filtro);
    Task<decimal> GetSomarAsync(DbContext ctx, DateTime data);
    Task<ResultCreateDto> SetRegistrarCredito(DbContext ctx, ExtratoCreateDto dados);
    Task<ResultCreateDto> SetRegistrarDebito(DbContext ctx, ExtratoCreateDto dados);
}
