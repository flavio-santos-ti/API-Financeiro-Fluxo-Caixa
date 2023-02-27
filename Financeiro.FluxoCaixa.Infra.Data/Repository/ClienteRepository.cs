using Financeiro.FluxoCaixa.Domain.DTO.Cliente;
using Financeiro.FluxoCaixa.Domain.DTO.Result;
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

public class ClienteRepository : BaseRepository, IClienteRepository
{
    public ClienteRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<ResultCreateDTO> SetIncluirAsync(DbContext ctx, long idPessoa)
    {
        var context = (DatabaseContext)ctx;

        try
        {
            Cliente clienteNew = new();
            clienteNew.PessoaId = idPessoa;
            clienteNew.DataInclusao = DateTime.Now;
            context.Clientes.Add(clienteNew);
            await context.SaveChangesAsync();

            return base.ResultCreate(true, "Cliente cadastrado com sucesso!", clienteNew.Id);
        }
        catch (Exception ex)
        {
            return base.ResultCreate(false, "Erro ao tentar cadastrar o cliente:" + ex.Message, 0);
        }
    }

    public async Task<Cliente> GetAsync(DbContext ctx, long idPessoa)
    {
        var context = (DatabaseContext)ctx;

        return await context.Clientes
            .Where(b => b.PessoaId == idPessoa)
            .FirstOrDefaultAsync();
    }

    public async Task<Cliente> GetNoTrackingAsync(DbContext ctx, long idCliente)
    {
        var context = (DatabaseContext)ctx;

        var cliente = await context.Clientes
            .Where(b => b.Id == idCliente)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return cliente;
    }

    public async Task<IEnumerable<ClienteDTO>> GetListarAsync()
    {
        const string sqlQuery = @"
        SELECT
          f.id AS Id,
          f.pessoa_id AS PessoaId,
          p.nome AS Nome,
          f.dt_inclusao AS DataInclusao
        FROM
          public.cliente f 
          INNER JOIN public.pessoa p ON (f.pessoa_id = p.id)
        "
        ;

        return await base.ListarAsync<ClienteDTO>(sqlQuery);
    }


}
