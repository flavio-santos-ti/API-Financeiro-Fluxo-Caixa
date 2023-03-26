using Financeiro.FluxoCaixa.Domain.Dtos.Result;
using Financeiro.FluxoCaixa.Domain.Dtos.TituloReceber;
using Financeiro.FluxoCaixa.Domain.Entities;
using Financeiro.FluxoCaixa.Domain.Interface.Repository;
using Financeiro.FluxoCaixa.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Infra.Data.Repository;

public class TituloReceberRepository : BaseRepository, ITituloReceberRepository
{
    public TituloReceberRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<ResultCreateDto> SetReceberAsync(DbContext ctx, TituloReceber dados)
    {
        var context = (DatabaseContext)ctx;

        try
        {
            dados.DataInclusao = DateTime.Now;
            context.TitulosReceber.Add(dados);
            await context.SaveChangesAsync();

            return base.ResultCreate(true, "Título a Receber registrado com sucesso!", dados.Id);
        }
        catch (Exception ex)
        {
            return base.ResultCreate(false, "Erro ao tentar registrar um título a receber: " + ex.Message, 0);
        }
    }

    public async Task<IEnumerable<TituloReceberDto>> GetListarAsync()
    {
        const string sqlQuery = @"
        SELECT
          t.id AS Id,
          t.cliente_id AS ClienteId,
          p.nome AS Origem,
          t.descricao AS Descricao,
          t.valor_real AS ValorReal,
          t.dt_real AS DataReal,
          t.dt_inclusao AS DataInclusao
        FROM
          titulo_receber t
          INNER JOIN cliente c ON (c.id = t.cliente_id)
          INNER JOIN pessoa p ON (p.id = c.pessoa_id)
        ";

        return await base.ListarAsync<TituloReceberDto>(sqlQuery);
    }

}
