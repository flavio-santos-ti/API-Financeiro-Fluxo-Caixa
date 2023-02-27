using Financeiro.FluxoCaixa.Domain.DTO.Extrato;
using Financeiro.FluxoCaixa.Domain.DTO.Result;
using Financeiro.FluxoCaixa.Domain.DTO.TituloReceber;
using Financeiro.FluxoCaixa.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Domain.Interface.Repository;

public interface IExtratoRepository
{
    Task<ResultCreateDTO> SetIncluirAsync(DbContext ctx, Extrato dados);
    Task<IEnumerable<ExtratoDTO>> GetListarAsync(ExtratoFiltroDTO filtro);
    Task<decimal> GetSomarAsync(ExtratoFiltroDTO filtro);
    Task<decimal> GetSomarAsync(DbContext ctx, DateTime data);
}
