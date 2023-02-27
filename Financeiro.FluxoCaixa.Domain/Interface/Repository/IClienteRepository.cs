using Financeiro.FluxoCaixa.Domain.DTO.Cliente;
using Financeiro.FluxoCaixa.Domain.DTO.Result;
using Financeiro.FluxoCaixa.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Domain.Interface.Repository;

public interface IClienteRepository
{
    Task<ResultCreateDTO> SetIncluirAsync(DbContext ctx, long idPessoa);
    Task<Cliente> GetAsync(DbContext ctx, long idPessoa);
    Task<Cliente> GetNoTrackingAsync(DbContext ctx, long idCliente);
    Task<IEnumerable<ClienteDTO>> GetListarAsync();
}
