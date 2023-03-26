using Financeiro.FluxoCaixa.Domain.Dtos.Caixa;
using Financeiro.FluxoCaixa.Domain.Dtos.Extrato;
using Financeiro.FluxoCaixa.Domain.Dtos.Result;
using Financeiro.FluxoCaixa.Domain.Dtos.TituloPagar;
using Financeiro.FluxoCaixa.Domain.Entities;
using Financeiro.FluxoCaixa.Domain.Interface.Service;
using Financeiro.FluxoCaixa.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

namespace Financeiro.FluxoCaixa.Business.Service;

public class CaixaService : BaseService, ICaixaService
{
    private readonly IConfiguration _configuration;
    private readonly ISaldoDiarioService _saldoDiarioService;
    private readonly IExtratoService _extratoService;

    public CaixaService(IConfiguration configuration, ISaldoDiarioService saldoDiarioService, IExtratoService extratoService)
    {
        _configuration = configuration;
        _saldoDiarioService = saldoDiarioService;
        _extratoService = extratoService;
    }

    private async Task<bool> IsAbertoAsync(DbContext ctx, DateTime data)
    {
        var saldoInicialExistente = await _saldoDiarioService.GetNoTrackingAsync(ctx, data, "I");

        return (saldoInicialExistente != null);
    }

    private async Task<bool> IsFechadoAsync(DbContext ctx, DateTime data)
    {
        var saldoFinalExistente = await _saldoDiarioService.GetNoTrackingAsync(ctx, data, "F");

        return (saldoFinalExistente != null);
    }

    public async Task<ResultCreateDto> SetAbrirAsync(CaixaCreateDto dados)
    {
        using (var context = new DatabaseContext(_configuration))
        {
            using (IDbContextTransaction transaction = await context.Database.BeginTransactionAsync())
            {
                dados.Data = dados.Data.Date;

                var situacao = await ParseAberturaAsync(context, dados.Data);

                if (!situacao.Successed)
                {
                    return situacao;
                }

                DateTime dataAnterior = dados.Data.AddDays(-1);
                var saldoDiarioAnterior = await _saldoDiarioService.GetNoTrackingAsync(context, dataAnterior, "F");

                decimal valor = 0;
                
                if (saldoDiarioAnterior != null)
                {
                    valor = saldoDiarioAnterior.Valor;
                }

                if (valor >= 0)
                {
                    ExtratoCreateDto extratoCaixaInicial = new();
                    extratoCaixaInicial.Descricao = "Saldo Anterior";
                    extratoCaixaInicial.Valor = valor;
                    extratoCaixaInicial.DataExtrato = dados.Data;
                    var extrato = await _extratoService.SetRegistrarCredito(context, extratoCaixaInicial);

                    if (extrato.Successed)
                    {
                        SaldoDiario saldoInicial = new();
                        saldoInicial.Tipo = "I";
                        saldoInicial.DataSaldo = dados.Data;
                        saldoInicial.Valor = valor;
                        saldoInicial.ExtratoId = extrato.Id;
                       
                        var saldo = await _saldoDiarioService.SetRegistrarAsync(context, saldoInicial);

                        if (saldo.Successed)
                        {
                            await transaction.CommitAsync();
                            return saldo;
                        } 
                        else
                        {
                            await transaction.RollbackAsync();
                            return saldo;
                        }
                    } 
                    else
                    {
                        await transaction.RollbackAsync();
                        return extrato;
                    }
                } 
                else
                {
                    TituloPagarCreateDto caixaInicial = new();
                    caixaInicial.Descricao = "Saldo Inicial";
                    caixaInicial.ValorReal = valor;
                    caixaInicial.DataReal = dados.Data;
                    var extrato = await _extratoService.SetRegistrarDebito(context, caixaInicial);

                    if (extrato.Successed)
                    {
                        SaldoDiario saldoInicial = new();
                        saldoInicial.Tipo = "I";
                        saldoInicial.DataSaldo = dados.Data;
                        saldoInicial.Valor = valor;
                        saldoInicial.ExtratoId = extrato.Id;

                        var saldo = await _saldoDiarioService.SetRegistrarAsync(context, saldoInicial);

                        if (saldo.Successed)
                        {
                            await transaction.CommitAsync();
                            return saldo;
                        }
                        else
                        {
                            await transaction.RollbackAsync();
                            return saldo;
                        }
                    }
                    else
                    {
                        await transaction.RollbackAsync();
                        return extrato;
                    }
                }
                
            }
        }

    }

    public async Task<ResultCreateDto> SetFecharAsync(CaixaCreateDto dados)
    {
        using (var context = new DatabaseContext(_configuration))
        {
            using (IDbContextTransaction transaction = await context.Database.BeginTransactionAsync())
            {
                dados.Data = dados.Data.Date;

                var saldoDiarioExistente = await _saldoDiarioService.GetNoTrackingAsync(context, dados.Data, "I");

                if (saldoDiarioExistente == null)
                {
                    await transaction.RollbackAsync();
                    return base.ResultCreate(false, "Não foi possível fechar o Caixa pois já encontra-se fechado ou ainda não foi aberto!", saldoDiarioExistente.Id);
                }
               
                decimal saldoAnterior = await _saldoDiarioService.GetSaldoAnteriorAsync(context, dados.Data);
                


                if (saldoAnterior >= 0)
                {
                    ExtratoCreateDto extratoSaldoDia = new();
                    extratoSaldoDia.Descricao = "Saldo do Dia";
                    extratoSaldoDia.Valor = saldoAnterior;
                    extratoSaldoDia.DataExtrato = dados.Data;

                    var extrato = await _extratoService.SetRegistrarCredito(context, extratoSaldoDia);

                    if (extrato.Successed)
                    {
                        SaldoDiario saldoInicial = new();
                        saldoInicial.Tipo = "F";
                        saldoInicial.DataSaldo = dados.Data;
                        saldoInicial.Valor = saldoAnterior;
                        saldoInicial.ExtratoId = extrato.Id;

                        var saldo = await _saldoDiarioService.SetRegistrarAsync(context, saldoInicial);

                        if (saldo.Successed)
                        {
                            await transaction.CommitAsync();
                            return saldo;
                        }
                        else
                        {
                            await transaction.RollbackAsync();
                            return saldo;
                        }
                    }
                    else
                    {
                        await transaction.RollbackAsync();
                        return extrato;
                    }
                }
                else
                {
                    ExtratoCreateDto extratoSaldoDia = new();
                    extratoSaldoDia.Descricao = "Saldo do Dia";
                    extratoSaldoDia.Valor = saldoAnterior;
                    extratoSaldoDia.DataExtrato = dados.Data;
                    var extrato = await _extratoService.SetRegistrarDebito(context, extratoSaldoDia);

                    if (extrato.Successed)
                    {
                        SaldoDiario saldoInicial = new();
                        saldoInicial.Tipo = "F";
                        saldoInicial.DataSaldo = dados.Data;
                        saldoInicial.Valor = saldoAnterior;
                        saldoInicial.ExtratoId = extrato.Id;

                        var saldo = await _saldoDiarioService.SetRegistrarAsync(context, saldoInicial);

                        if (saldo.Successed)
                        {
                            await transaction.CommitAsync();
                            return saldo;
                        }
                        else
                        {
                            await transaction.RollbackAsync();
                            return saldo;
                        }
                    }
                    else
                    {
                        await transaction.RollbackAsync();
                        return extrato;
                    }
                }

            }
        }

    }

    public async Task<ResultCreateDto> ParseLancamentoAsync(DbContext ctx, DateTime data)
    {
        var context = (DatabaseContext)ctx;
        
        data = data.Date;
        string invalidMessage = string.Empty;

        bool caixaFechado = await IsFechadoAsync(context, data);

        if (caixaFechado)
            invalidMessage = "Não é possível registrar um lançamento com o caixa fechado!";

        bool caixaAberto = await IsAbertoAsync(context, data);

        if (!caixaAberto) 
            invalidMessage = "É necessário abrir o caixa para efetuar um lanlamento!";

        return base.ResultCreate((invalidMessage == string.Empty), invalidMessage, 0);
    }

    public async Task<ResultCreateDto> ParseAberturaAsync(DbContext ctx, DateTime data)
    {
        var context = (DatabaseContext)ctx;

        data = data.Date;
        string invalidMessage = string.Empty;

        bool caixaFechado = await IsFechadoAsync(context, data);

        if (caixaFechado)
            invalidMessage = "Não é possível realizar a abertura com um caixa já fechado!";

        bool caixaAberto = await IsAbertoAsync(context, data);

        if (caixaAberto)
            invalidMessage = "Não é possível realizar a abertura de um caixa já aberto";

        return base.ResultCreate((invalidMessage == string.Empty), invalidMessage, 0);
    }


    public async Task<CaixaDto> GetSituacaoAsync()
    {
        var saldoDiario = await _saldoDiarioService.GetSituacaoAsync();

        string situacao = string.Empty;

        if (saldoDiario != null)
        {
            if (saldoDiario.Valor < 0)
                situacao = "Saldo devedor";

            if (saldoDiario.Valor > 0)
                situacao = "Saldo positivo";

            if (saldoDiario.Valor == 0)
                situacao = "Saldo zerado";
        }
        else situacao = "Sem movimento";
            
        CaixaDto caixa = new();
        caixa.Situacao = situacao;

        if (caixa.Situacao != "Sem movimento")
        {
            caixa.DataUltimoMovimento = saldoDiario.DataSaldo;
            caixa.Saldo = saldoDiario.Valor;
            caixa.SaldoId = saldoDiario.Id;
        } 
        else
        {
            caixa.DataUltimoMovimento = DateTime.Now.Date;
            caixa.Saldo = 0;
            caixa.SaldoId = 0;
        }

        return caixa;
    }
}
