using Financeiro.FluxoCaixa.Domain.DTO.Result;
using Financeiro.FluxoCaixa.Domain.DTO.TituloPagar;
using Financeiro.FluxoCaixa.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Domain.Interface.Repository;

public interface ITituloPagarRepository
{
    Task<ResultCreateDTO> SetPagarAsync(DbContext ctx, TituloPagar dados);
    Task<IEnumerable<TituloPagarDTO>> GetListarAsync();
}
