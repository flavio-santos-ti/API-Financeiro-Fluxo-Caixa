using Financeiro.FluxoCaixa.Domain.Dtos.Result;
using Financeiro.FluxoCaixa.Domain.Dtos.TituloReceber;
using Financeiro.FluxoCaixa.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Financeiro.FluxoCaixa.Domain.Interface.Repository
{
    public interface ITituloReceberRepository
    {
        Task<ResultCreateDto> SetReceberAsync(DbContext ctx, TituloReceber dados);
        Task<IEnumerable<TituloReceberDto>> GetListarAsync();
    }
}
