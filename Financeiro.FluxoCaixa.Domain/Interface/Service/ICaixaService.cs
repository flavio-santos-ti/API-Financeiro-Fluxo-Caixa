using Financeiro.FluxoCaixa.Domain.Dtos.Caixa;
using Financeiro.FluxoCaixa.Domain.Dtos.Result;
using Microsoft.EntityFrameworkCore;

namespace Financeiro.FluxoCaixa.Domain.Interface.Service;

public interface ICaixaService
{
    Task<ResultCreateDto> SetAbrirAsync(CaixaCreateDto dados);
    Task<ResultCreateDto> SetFecharAsync(CaixaCreateDto dados);
    Task<ResultCreateDto> ParseLancamentoAsync(DbContext ctx, DateTime data);
    Task<CaixaDto> GetSituacaoAsync();
}
