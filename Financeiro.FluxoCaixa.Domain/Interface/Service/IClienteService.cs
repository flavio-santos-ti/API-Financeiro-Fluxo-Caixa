using Financeiro.FluxoCaixa.Domain.Dtos.Cliente;
using Financeiro.FluxoCaixa.Domain.Dtos.Result;
using Microsoft.EntityFrameworkCore;


namespace Financeiro.FluxoCaixa.Domain.Interface.Service;

public interface IClienteService
{
    Task<ResultCreateDto> SetCadastrarAsync(ClienteCreateDto dados);
    Task<IEnumerable<ClienteDto>> GetListarAsync();
    Task<ResultCreateDto> ParseAsync(DbContext ctx, long id);
}
