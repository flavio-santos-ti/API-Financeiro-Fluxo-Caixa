﻿using Financeiro.FluxoCaixa.Domain.Dtos.Result;
using Financeiro.FluxoCaixa.Domain.Dtos.TituloPagar;
using Financeiro.FluxoCaixa.Domain.Entities;
using Financeiro.FluxoCaixa.Domain.Interface.Repositories;
using Financeiro.FluxoCaixa.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Financeiro.FluxoCaixa.Infra.Data.Repositories;

public class TituloPagarRepository : BaseRepository, ITituloPagarRepository
{
    public TituloPagarRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<ResultCreateDto> SetPagarAsync(DbContext ctx, TituloPagar dados)
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

    public async Task<IEnumerable<TituloPagarDto>> GetListarAsync()
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

        return await base.ListarAsync<TituloPagarDto>(sqlQuery);
    }

   

}
