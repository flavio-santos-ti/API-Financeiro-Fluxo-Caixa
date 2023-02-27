using Financeiro.FluxoCaixa.Domain.DTO.Caixa;
using Financeiro.FluxoCaixa.Domain.DTO.Result;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Domain.Interface.Service;

public interface ICaixaService
{
    Task<ResultCreateDTO> SetAbrirAsync(CaixaCreateDTO dados);
    Task<ResultCreateDTO> SetFecharAsync(CaixaCreateDTO dados);
    Task<ResultCreateDTO> ParseLancamentoAsync(DbContext ctx, DateTime data);
    Task<CaixaDTO> GetSituacaoAsync();
}
