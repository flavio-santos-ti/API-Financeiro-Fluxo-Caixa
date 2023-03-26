using Financeiro.FluxoCaixa.Domain.Dtos.Pessoa;
using Financeiro.FluxoCaixa.Domain.Dtos.Result;
using Microsoft.EntityFrameworkCore;

namespace Financeiro.FluxoCaixa.Domain.Interface.Service;

public interface IPessoaService
{
    Task<ResultCreateDto> SetIncluirAsync(DbContext ctx, PessoaCreateDto dados);
}
