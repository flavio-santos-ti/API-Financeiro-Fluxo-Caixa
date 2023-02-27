using Financeiro.FluxoCaixa.Domain.DTO.Result;
using Financeiro.FluxoCaixa.Domain.DTO.SaldoDiario;
using Financeiro.FluxoCaixa.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Domain.Interface.Repository;

public interface ISaldoDiarioRepository
{
    Task<ResultCreateDTO> SetIncluirAsync(DbContext ctx, SaldoDiario dados);
    Task<IEnumerable<SaldoDiarioDTO>> GetListarAsync(SaldoDiarioFiltroDTO filtro);
    Task<SaldoDiario> GetNoTrackingAsync(DbContext ctx, DateTime data, string tipo);
    Task<SaldoDiario> GetSituacaoAsync();
    Task<SaldoDiario> GetAsync(DbContext ctx, DateTime data);
    Task<decimal> GetSaldoAnteriorAsync(DbContext ctx, DateTime data);
}
