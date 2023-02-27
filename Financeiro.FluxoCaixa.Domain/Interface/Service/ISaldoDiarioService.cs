using Financeiro.FluxoCaixa.Domain.DTO.Result;
using Financeiro.FluxoCaixa.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Domain.Interface.Service;

public interface ISaldoDiarioService
{
    Task<SaldoDiario> GetNoTrackingAsync(DbContext ctx, DateTime data, string tipo);
    Task<ResultCreateDTO> SetRegistrarAsync(DbContext ctx, SaldoDiario dados);
    Task<SaldoDiario> GetSituacaoAsync();
    Task<decimal> GetSaldoAnteriorAsync(DbContext ctx, DateTime data);
}
