using Financeiro.FluxoCaixa.Domain.Dtos.Result;
using Financeiro.FluxoCaixa.Domain.Dtos.TituloPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Domain.Interface.Service;

public interface ITituloPagarService
{
    Task<ResultCreateDto> SetPagarAsync(TituloPagarCreateDto dados);
    Task<IEnumerable<TituloPagarDto>> GetListarAsync();
}
