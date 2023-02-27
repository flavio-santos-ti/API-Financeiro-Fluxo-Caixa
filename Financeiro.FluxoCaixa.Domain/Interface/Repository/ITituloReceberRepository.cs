using Financeiro.FluxoCaixa.Domain.DTO.Result;
using Financeiro.FluxoCaixa.Domain.DTO.TituloReceber;
using Financeiro.FluxoCaixa.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Domain.Interface.Repository
{
    public interface ITituloReceberRepository
    {
        Task<ResultCreateDTO> SetReceberAsync(DbContext ctx, TituloReceber dados);
        Task<IEnumerable<TituloReceberDTO>> GetListarAsync();
    }
}
