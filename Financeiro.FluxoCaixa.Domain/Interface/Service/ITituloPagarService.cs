using Financeiro.FluxoCaixa.Domain.DTO.Result;
using Financeiro.FluxoCaixa.Domain.DTO.TituloPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Domain.Interface.Service;

public interface ITituloPagarService
{
    Task<ResultCreateDTO> SetPagarAsync(TituloPagarCreateDTO dados);
    Task<IEnumerable<TituloPagarDTO>> GetListarAsync();
}
