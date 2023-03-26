using Financeiro.FluxoCaixa.Domain.Dtos.Result;
using Financeiro.FluxoCaixa.Domain.Dtos.TituloReceber;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Domain.Interface.Service;

public interface ITituloReceberService
{
    Task<ResultCreateDto> SetReceberAsync(TituloReceberCreateDto dados);
    Task<IEnumerable<TituloReceberDto>> GetListarAsync();
}
