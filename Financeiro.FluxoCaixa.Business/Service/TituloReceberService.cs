using AutoMapper;
using Financeiro.FluxoCaixa.Domain.Dtos.Result;
using Financeiro.FluxoCaixa.Domain.Dtos.TituloReceber;
using Financeiro.FluxoCaixa.Domain.Entities;
using Financeiro.FluxoCaixa.Domain.Interface.Repository;
using Financeiro.FluxoCaixa.Domain.Interface.Service;
using Financeiro.FluxoCaixa.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

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


    public async Task<ResultCreateDto> ParseAsync(DbContext ctx, TituloReceberCreateDto dados)
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

    public async Task<ResultCreateDto> SetReceberAsync(TituloReceberCreateDto dados)
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

    public async Task<IEnumerable<TituloReceberDto>> GetListarAsync()
    {
        return await _tituloReceberRepository.GetListarAsync();
    }
}
