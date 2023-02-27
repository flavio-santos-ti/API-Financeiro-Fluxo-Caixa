using Financeiro.FluxoCaixa.Domain.DTO.Extrato;
using Financeiro.FluxoCaixa.Domain.DTO.Result;
using Financeiro.FluxoCaixa.Domain.DTO.TituloPagar;
using Financeiro.FluxoCaixa.Domain.DTO.TituloReceber;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Domain.Interface.Service;

public interface IExtratoService
{
    Task<ResultCreateDTO> SetRegistrarDebito(DbContext ctx, TituloPagarCreateDTO dados);
    Task<ResultCreateDTO> SetRegistrarCredito(DbContext ctx, TituloReceberCreateDTO dados);
    Task<IEnumerable<ExtratoDTO>> GetListarAsync(ExtratoFiltroDTO filtro);
    Task<decimal> GetSomarAsync(ExtratoFiltroDTO filtro);
    Task<decimal> GetSomarAsync(DbContext ctx, DateTime data);
    Task<ResultCreateDTO> SetRegistrarCredito(DbContext ctx, ExtratoCreateDTO dados);
    Task<ResultCreateDTO> SetRegistrarDebito(DbContext ctx, ExtratoCreateDTO dados);
}
