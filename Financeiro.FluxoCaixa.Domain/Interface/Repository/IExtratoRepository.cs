using Financeiro.FluxoCaixa.Domain.Dtos.Extrato;
using Financeiro.FluxoCaixa.Domain.Dtos.Result;
using Financeiro.FluxoCaixa.Domain.Dtos.TituloReceber;
using Financeiro.FluxoCaixa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Domain.Interface.Repository;

public interface IExtratoRepository
{
    Task<ResultCreateDto> SetIncluirAsync(DbContext ctx, Extrato dados);
    Task<IEnumerable<ExtratoDto>> GetListarAsync(ExtratoFiltroDto filtro);
    Task<decimal> GetSomarAsync(ExtratoFiltroDto filtro);
    Task<decimal> GetSomarAsync(DbContext ctx, DateTime data);
}
