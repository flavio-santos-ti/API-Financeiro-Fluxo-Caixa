using Financeiro.FluxoCaixa.Domain.DTO.Result;
using Financeiro.FluxoCaixa.Domain.DTO.TituloReceber;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Domain.Interface.Service;

public interface ITituloReceberService
{
    Task<ResultCreateDTO> SetReceberAsync(TituloReceberCreateDTO dados);
    Task<IEnumerable<TituloReceberDTO>> GetListarAsync();
}
