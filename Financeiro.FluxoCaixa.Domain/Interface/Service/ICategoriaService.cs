using Financeiro.FluxoCaixa.Domain.Dtos.Categoria;
using Financeiro.FluxoCaixa.Domain.Dtos.Result;
using Microsoft.EntityFrameworkCore;

namespace Financeiro.FluxoCaixa.Domain.Interface.Service;

public interface ICategoriaService
{
    Task<ResultCreateDto> SetCadastrarAsync(CategoriaCreateDto dados);
    Task<ResultDeleteDto> SetRemoverAsync(int id);
    Task<IEnumerable<CategoriaDto>> GetListarAsync();
    Task<ResultCreateDto> ParseAsync(DbContext ctx, int id);
}
