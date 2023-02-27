using Financeiro.FluxoCaixa.Domain.DTO.Extrato;
using Financeiro.FluxoCaixa.Domain.DTO.Result;
using Financeiro.FluxoCaixa.Domain.DTO.SaldoDiario;
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

public class SaldoDiarioRepository : BaseRepository, ISaldoDiarioRepository
{
    public SaldoDiarioRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<ResultCreateDTO> SetIncluirAsync(DbContext ctx, SaldoDiario dados)
    {
        var context = (DatabaseContext)ctx;

        try
        {
            var saldo = await context.SaldosDiarios
                .Where(b => b.DataSaldo == dados.DataSaldo && b.Tipo == dados.Tipo)
                .FirstOrDefaultAsync();

            if (saldo == null)
            {
                dados.DataInclusao = DateTime.Now;
                context.SaldosDiarios.Add(dados);
                await context.SaveChangesAsync();
            } else
            {
                saldo.DataSaldo = dados.DataSaldo;
                saldo.Valor = dados.Valor;
                saldo.ExtratoId= dados.ExtratoId;
                saldo.DataInclusao = DateTime.Now.Date;
                await context.SaveChangesAsync();
            }

            return base.ResultCreate(true, "Saldo Diário registrado com sucesso!", dados.Id);
        }
        catch (Exception ex)
        {
            return base.ResultCreate(false, "Erro ao tentar registrar um Saldo Diário: " + ex.Message, 0);
        }
    }

    public async Task<decimal> GetSaldoAnteriorAsync(DbContext ctx, DateTime data)
    {
        var context = (DatabaseContext)ctx;

        var saldo = await context.SaldosDiarios
            .Where(b => b.DataSaldo <= data)
            .AsNoTracking()
            .OrderByDescending(b => b.Id)
            .FirstOrDefaultAsync();

        if (saldo == null) return 0;

        return saldo.Valor;
    }


    public async Task<SaldoDiario> GetAsync(DbContext ctx, DateTime data)
    {
        var context = (DatabaseContext)ctx;
        
        var saldo = await context.SaldosDiarios
            .Where(b => b.DataSaldo == data)
            .FirstOrDefaultAsync();
        
        return saldo;
    }

    public async Task<IEnumerable<SaldoDiarioDTO>> GetListarAsync(SaldoDiarioFiltroDTO filtro)
    {
        const string sqlQuery = @"
        SELECT
          id AS Id,
          dt_saldo AS DataSaldo,
          valor AS Valor,
          dt_inclusao AS DataInclusao,
          extrato_id AS ExtratoId
        FROM
          saldo_diario
        WHERE
          AND dt_saldo BETWEEN @DataInicial AND @DataFinal
        ";

        return await base.ListarAsync<SaldoDiarioDTO>(sqlQuery, filtro);
    }

    public async Task<SaldoDiario> GetNoTrackingAsync(DbContext ctx, DateTime data, string tipo)
    {
        var context = (DatabaseContext)ctx;

        var saldoDiario = await context.SaldosDiarios
            .Where(b => b.DataSaldo == data && b.Tipo == tipo)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return saldoDiario;

    }

    public async Task<SaldoDiario> GetSituacaoAsync()
    {
        const string sqlQuery = @"
        SELECT
          id AS Id,
          dt_saldo AS DataSaldo,
          tipo AS Tipo,
          valor AS Valor,
          dt_inclusao AS DataInclusao,
          extrato_id AS ExtratoId
        FROM
          saldo_diario
        WHERE
          id = (SELECT MAX(id) FROM saldo_diario)
        ";

        return await base.FirstOrDefaultAsync<SaldoDiario>(sqlQuery);
    }


}
