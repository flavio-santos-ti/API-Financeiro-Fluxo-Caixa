using Financeiro.FluxoCaixa.Domain.DTO.Categoria;
using Financeiro.FluxoCaixa.Domain.DTO.Result;
using Financeiro.FluxoCaixa.Domain.Entity;
using Financeiro.FluxoCaixa.Domain.Interface.Repository;
using Financeiro.FluxoCaixa.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Infra.Data.Repository;

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

    public async Task<ResultCreateDTO> SetIncluirAsync(DbContext ctx, Categoria dados)
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

    public async Task<ResultDeleteDTO> SetExcluirAsync(DbContext ctx, int id)
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
