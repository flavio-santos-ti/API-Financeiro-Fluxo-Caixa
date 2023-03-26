using Financeiro.FluxoCaixa.Domain.Dtos.Fornecedor;
using Financeiro.FluxoCaixa.Domain.Entities;
using Financeiro.FluxoCaixa.Domain.Interface.Repository;
using Financeiro.FluxoCaixa.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Financeiro.FluxoCaixa.Infra.Data.Repository;

public class FornecedorRepository : BaseRepository, IFornecedorRepository
{
    public FornecedorRepository(IConfiguration configuration): base(configuration)
    {
    }

    public async Task<Fornecedor> GetNoTrackingAsync(DbContext ctx, long idFornecedor)
    {
        var context = (DatabaseContext)ctx;

        var fornecedor = await context.Fornecedores
            .Where(b => b.Id == idFornecedor)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return fornecedor;
    }

    public async Task<IEnumerable<FornecedorDto>> GetListarAsync()
    {
        const string sqlQuery = @"
        SELECT
          f.id AS Id,
          f.pessoa_id AS PessoaId,
          p.nome AS Nome,
          f.dt_inclusao AS DataInclusao
        FROM
          public.fornecedor f 
          INNER JOIN public.pessoa p ON (f.pessoa_id = p.id)
        "
        ;

        return await base.ListarAsync<FornecedorDto>(sqlQuery);
    }


}
