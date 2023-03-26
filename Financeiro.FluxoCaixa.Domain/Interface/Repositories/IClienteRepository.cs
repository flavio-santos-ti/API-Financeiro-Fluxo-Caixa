using Financeiro.FluxoCaixa.Domain.Dtos.Cliente;
using Financeiro.FluxoCaixa.Domain.Dtos.Result;
using Financeiro.FluxoCaixa.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Financeiro.FluxoCaixa.Domain.Interface.Repositories;

public interface IClienteRepository
{
    Task<ResultCreateDto> SetIncluirAsync(DbContext ctx, long idPessoa);
    Task<Cliente> GetAsync(DbContext ctx, long idPessoa);
    Task<Cliente> GetNoTrackingAsync(DbContext ctx, long idCliente);
    Task<IEnumerable<ClienteDto>> GetListarAsync();
}
