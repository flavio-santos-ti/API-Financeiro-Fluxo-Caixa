using Financeiro.FluxoCaixa.Domain.Dtos.Result;
using Financeiro.FluxoCaixa.Domain.Dtos.TituloPagar;
using Financeiro.FluxoCaixa.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Financeiro.FluxoCaixa.Domain.Interface.Repositories;

public interface ITituloPagarRepository
{
    Task<ResultCreateDto> SetPagarAsync(DbContext ctx, TituloPagar dados);
    Task<IEnumerable<TituloPagarDto>> GetListarAsync();
}
