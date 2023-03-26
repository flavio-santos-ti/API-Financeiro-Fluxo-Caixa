using Financeiro.FluxoCaixa.Domain.Dtos.Result;
using Financeiro.FluxoCaixa.Domain.Dtos.TituloPagar;

namespace Financeiro.FluxoCaixa.Domain.Interface.Service;

public interface ITituloPagarService
{
    Task<ResultCreateDto> SetPagarAsync(TituloPagarCreateDto dados);
    Task<IEnumerable<TituloPagarDto>> GetListarAsync();
}
