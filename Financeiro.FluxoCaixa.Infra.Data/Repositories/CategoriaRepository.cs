using Financeiro.FluxoCaixa.Domain.Dtos.Result;
using Financeiro.FluxoCaixa.Domain.Entities;
using Financeiro.FluxoCaixa.Domain.Interface.Repositories;
using Financeiro.FluxoCaixa.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Financeiro.FluxoCaixa.Infra.Data.Repositories;

public class CategoriaRepository : BaseRepository, ICategoriaRepository
{
    public CategoriaRepository()
    {
    }
    
    public async Task<Categoria> GetNoTrackingAsync(DbContext ctx, long idCategoria)
    {
        var context = (DatabaseContext)ctx;

        var categoria = await context.Categorias
            .Where(b => b.Id == idCategoria)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return categoria;
    }

    public async Task<Categoria> GetNoTrackingAsync(DbContext ctx, string nome)
    {
        var context = (DatabaseContext)ctx;

        var categoria = await context.Categorias
            .Where(b => b.Nome == nome)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return categoria;
    }

    public async Task<ResultCreateDto> SetIncluirAsync(DbContext ctx, Categoria dados)
    {
        try
        {
            var context = (DatabaseContext)ctx;
            context.Categorias.Add(dados);
            await context.SaveChangesAsync();

            return base.ResultCreate(true, "Categoria registrada com sucesso!", dados.Id);
        }
        catch (Exception ex)
        {
            return base.ResultCreate(false, "Erro ao tentar registrar uma categoria: " + ex.Message, 0);
        }
    }

    public async Task<ResultDeleteDto> SetExcluirAsync(DbContext ctx, int id)
    {
        try
        {
            var context = (DatabaseContext)ctx;
            var categoria = await context.Categorias
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();

            context.Categorias.Remove(categoria);
            await context.SaveChangesAsync();
            
            return base.ResultDelete(true, "Categoria removida com sucesso!", 1);
        }
        catch (Exception ex)
        {
            return base.ResultDelete(false, "Erro ao tentar registrar uma categoria: " + ex.Message, 0);
        }
    }


    public async Task<IEnumerable<Categoria>> GetListarAsync(DbContext ctx)
    {
        var context = (DatabaseContext)ctx;
        var categorias = await context.Categorias
            .AsNoTracking()
            .ToListAsync();

        return categorias;
    }


}
