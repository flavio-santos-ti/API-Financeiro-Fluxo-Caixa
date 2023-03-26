using Financeiro.FluxoCaixa.Domain.Dtos.Fornecedor;
using Financeiro.FluxoCaixa.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Financeiro.FluxoCaixa.Domain.Interface.Repository;

public interface IFornecedorRepository
{
    Task<Fornecedor> GetNoTrackingAsync(DbContext ctx, long idFornecedor);
    Task<IEnumerable<FornecedorDto>> GetListarAsync();
}
