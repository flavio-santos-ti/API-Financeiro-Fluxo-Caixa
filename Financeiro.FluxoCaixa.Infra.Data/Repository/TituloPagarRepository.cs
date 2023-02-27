using Financeiro.FluxoCaixa.Domain.DTO.Result;
using Financeiro.FluxoCaixa.Domain.DTO.TituloPagar;
using Financeiro.FluxoCaixa.Domain.Entity;
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

public class TituloPagarRepository : BaseRepository, ITituloPagarRepository
{
    public TituloPagarRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<ResultCreateDTO> SetPagarAsync(DbContext ctx, TituloPagar dados)
    {
        var context = (DatabaseContext)ctx;

        try
        {
            dados.DataInclusao = DateTime.Now;
            context.TitulosPagar.Add(dados);
            await context.SaveChangesAsync();

            return base.ResultCreate(true, "Título a Pagar registrado com sucesso!", dados.Id);
        }
        catch (Exception ex)
        {
            return base.ResultCreate(false, "Erro ao tentar registrar um título a pagar: " + ex.Message, 0);
        }
    }

    public async Task<IEnumerable<TituloPagarDTO>> GetListarAsync()
    {
        const string sqlQuery = @"
        SELECT
          t.id AS Id,
          t.fornecedor_id AS FornecedorId,
          p.nome AS Favorecido,
          t.descricao AS Descricao,
          t.valor_real AS ValorReal,
          t.dt_real AS DataReal,
          t.dt_inclusao AS DataInclusao
        FROM
          titulo_pagar t
          INNER JOIN fornecedor f ON (f.id = t.fornecedor_id)
          INNER JOIN pessoa p ON (p.id = f.pessoa_id)
        ";

        return await base.ListarAsync<TituloPagarDTO>(sqlQuery);
    }

   

}
