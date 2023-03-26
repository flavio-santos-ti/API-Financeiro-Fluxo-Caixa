using Financeiro.FluxoCaixa.Domain.Dtos.Fornecedor;
using Financeiro.FluxoCaixa.Domain.Dtos.Result;
using Microsoft.EntityFrameworkCore;

namespace Financeiro.FluxoCaixa.Domain.Interface.Service;

public interface IFornecedorService
{
    Task<ResultCreateDto> SetCadastrarAsync(FornecedorCreateDto dados);
    Task<IEnumerable<FornecedorDto>> GetListarAsync();
    Task<ResultCreateDto> ParseAsync(DbContext ctx, long id);
}
