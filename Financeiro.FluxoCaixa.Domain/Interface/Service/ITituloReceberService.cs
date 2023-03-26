using Financeiro.FluxoCaixa.Domain.Dtos.Result;
using Financeiro.FluxoCaixa.Domain.Dtos.TituloReceber;

namespace Financeiro.FluxoCaixa.Domain.Interface.Service;

public interface ITituloReceberService
{
    Task<ResultCreateDto> SetReceberAsync(TituloReceberCreateDto dados);
    Task<IEnumerable<TituloReceberDto>> GetListarAsync();
}
