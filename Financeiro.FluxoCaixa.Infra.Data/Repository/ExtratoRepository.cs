using Financeiro.FluxoCaixa.Domain.Dtos.Extrato;
using Financeiro.FluxoCaixa.Domain.Dtos.Result;
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

public class ExtratoRepository : BaseRepository, IExtratoRepository
{

    public ExtratoRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<ResultCreateDto> SetIncluirAsync(DbContext ctx, Extrato dados)
    {
        try
        {
            var context = (DatabaseContext)ctx;
            
            context.Extratos.Add(dados);
            await context.SaveChangesAsync();

            string tipo = dados.Tipo == "C" ? "Crédito" : "Débito";

            return base.ResultCreate(true, $"{tipo} registrado no extrato com sucesso!", dados.Id);
        }
        catch (Exception ex)
        {
            string tipo = dados.Tipo == "C" ? "Crédito" : "Débito";
            
            return base.ResultCreate(false, $"Erro ao tentar registrar um {tipo} no extrato: " + ex.Message, 0);
        }
    }

    public async Task<IEnumerable<ExtratoDto>> GetListarAsync(ExtratoFiltroDto filtro)
    {
        const string sqlQuery = @"
        SELECT 
          *
        FROM
          (
		    SELECT
              ext.id AS Id,
		      ext.tipo AS Tipo,
		      ext.descricao AS Descricao,
		      ext.valor AS Valor,
		      ext.dt_extrato as DataExtrato,
              tpg.id AS TituloId,
              'Destino: '||psf.nome AS Nome
            FROM
              extrato ext
              LEFT JOIN titulo_pagar tpg ON (tpg.extrato_id = ext.id)    
              LEFT JOIN fornecedor fnc ON (fnc.id = tpg.fornecedor_id)
              LEFT JOIN pessoa psf ON (psf.id = fnc.pessoa_id)
            WHERE
              ext.tipo = 'D'
              AND ext.dt_extrato BETWEEN @DataInicial AND @DataFinal
            UNION 
            SELECT
              ext.id AS Id,
		      ext.tipo AS Tipo,
		      ext.descricao AS Descricao,
		      ext.valor AS Valor,
		      ext.dt_extrato as DataExtrato,
              trc.id AS TituloId,
              'Origem: '||psc.nome AS Nome
            FROM
              extrato ext
              LEFT JOIN titulo_receber trc ON (trc.extrato_id = ext.id)    
              LEFT JOIN cliente cli ON (cli.id = trc.cliente_id)
              LEFT JOIN pessoa psc ON (psc.id = cli.pessoa_id)
            WHERE
              ext.tipo = 'C'
              AND ext.dt_extrato BETWEEN @DataInicial AND @DataFinal
          ) VI
        ORDER BY
          VI.Id
        ";

        return await base.ListarAsync<ExtratoDto>(sqlQuery, filtro);
    }

    public async Task<decimal> GetSomarAsync(ExtratoFiltroDto filtro)
    {
        const string sqlQuery = @"
        SELECT
          SUM(valor) as Valor
        FROM
          extrato
        WHERE
          dt_extrato BETWEEN @DataInicial AND @DataFinal
        ";
        return await base.GetFirstOrDefaultAsync<decimal>(sqlQuery, filtro);
    }

    public async Task<decimal> GetSomarAsync(DbContext ctx, DateTime data)
    {
        var context = (DatabaseContext)ctx;

        decimal total = await context.Extratos
            .Where(b => b.DataExtrato >= data.AddDays(-1) && b.DataExtrato >= data)
            .AsNoTracking()
            .SumAsync(b => b.Valor);

        return total;
    }

}
