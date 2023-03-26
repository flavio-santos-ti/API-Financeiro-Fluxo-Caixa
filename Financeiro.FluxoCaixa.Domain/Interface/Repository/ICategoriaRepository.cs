using Financeiro.FluxoCaixa.Domain.Dtos.Result;
using Financeiro.FluxoCaixa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Domain.Interface.Repository;

public interface ICategoriaRepository
{
    Task<Categoria> GetNoTrackingAsync(DbContext ctx, long idCategoria);
    Task<Categoria> GetNoTrackingAsync(DbContext ctx, string nome);
    Task<ResultCreateDto> SetIncluirAsync(DbContext ctx, Categoria dados);
    Task<ResultDeleteDto> SetExcluirAsync(DbContext ctx, int id);
    Task<IEnumerable<Categoria>> GetListarAsync(DbContext ctx);
}
