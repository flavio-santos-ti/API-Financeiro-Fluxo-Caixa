using AutoMapper;
using Financeiro.FluxoCaixa.Domain.DTO.Result;
using Financeiro.FluxoCaixa.Domain.DTO.TituloReceber;
using Financeiro.FluxoCaixa.Domain.Entity;
using Financeiro.FluxoCaixa.Domain.Interface.Repository;
using Financeiro.FluxoCaixa.Domain.Interface.Service;
using Financeiro.FluxoCaixa.Infra.Data.Context;
using Financeiro.FluxoCaixa.Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Business.Service;

public class TituloReceberService : BaseService, ITituloReceberService
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly IClienteService _clienteService;
    private readonly ICategoriaService _categoriaService;
    private readonly IExtratoService _extratoService;
    private readonly ITituloReceberRepository _tituloReceberRepository;
    private readonly ICaixaService _caixaService;
    private readonly ISaldoDiarioService _saldoDiarioService;

    public TituloReceberService(IConfiguration configuration, IMapper mapper, IClienteService clienteService, ICategoriaService categoriaService, ITituloReceberRepository tituloReceberRepository, IExtratoService extratoService, ICaixaService caixaService, ISaldoDiarioService saldoDiarioService)
    {
        _configuration = configuration;
        _mapper = mapper;
        _clienteService = clienteService;
        _categoriaService = categoriaService;
        _tituloReceberRepository = tituloReceberRepository;
        _extratoService = extratoService;
        _caixaService = caixaService;
        _saldoDiarioService = saldoDiarioService;
    }


    public async Task<ResultCreateDTO> ParseAsync(DbContext ctx, TituloReceberCreateDTO dados)
    {
        var context = (DatabaseContext)ctx;

        var situacaoCaixa = await _caixaService.ParseLancamentoAsync(context, dados.DataReal);

        if (!situacaoCaixa.Successed) 
            return situacaoCaixa;

        var situacaoCliente = await _clienteService.ParseAsync(context, dados.ClienteId);

        if (!situacaoCliente.Successed) 
            return situacaoCliente;

        var situacaoCategoria = await _categoriaService.ParseAsync(context, dados.CategoriaId);

        if (!situacaoCategoria.Successed) 
            return situacaoCategoria;

        return base.ResultCreate(true, string.Empty, 0);
    }

    public async Task<ResultCreateDTO> SetReceberAsync(TituloReceberCreateDTO dados)
    {
        using (var context = new DatabaseContext(_configuration))
        {
            using (IDbContextTransaction transaction = await context.Database.BeginTransactionAsync())
            {
                dados.Descricao = ClearString(dados.Descricao.ToUpper());
                dados.DataReal = dados.DataReal.Date;

                var situacao = await ParseAsync(context, dados);

                if (!situacao.Successed) 
                    return situacao;

                var extrato = await _extratoService.SetRegistrarCredito(context, dados);

                if (!extrato.Successed)
                    return extrato;

                decimal saldoAnterior = await _saldoDiarioService.GetSaldoAnteriorAsync(context, dados.DataReal);

                SaldoDiario saldoMovimento = new();
                saldoMovimento.Tipo = "M";
                saldoMovimento.DataSaldo = dados.DataReal;
                saldoMovimento.Valor = saldoAnterior + dados.ValorReal;
                saldoMovimento.ExtratoId = extrato.Id;

                var saldo = await _saldoDiarioService.SetRegistrarAsync(context, saldoMovimento);

                if (!saldo.Successed)
                {
                    await transaction.RollbackAsync();
                    return saldo;
                }

                var titulo = _mapper.Map<TituloReceber>(dados);

                titulo.ExtratoId = extrato.Id;

                var tituloNew = await _tituloReceberRepository.SetReceberAsync(context, titulo);

                if (!tituloNew.Successed)
                {
                    await transaction.RollbackAsync();
                    return tituloNew;
                }

                await transaction.CommitAsync();
                return tituloNew;
            }

        }
    }

    public async Task<IEnumerable<TituloReceberDTO>> GetListarAsync()
    {
        return await _tituloReceberRepository.GetListarAsync();
    }
}
