using Financeiro.FluxoCaixa.Domain.DTO.Cliente;
using Financeiro.FluxoCaixa.Domain.DTO.Result;
using Financeiro.FluxoCaixa.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Financeiro.FluxoCaixa.Domain.Interface.Service;

public interface IClienteService
{
    Task<ResultCreateDTO> SetCadastrarAsync(ClienteCreateDTO dados);
    Task<IEnumerable<ClienteDTO>> GetListarAsync();
    Task<ResultCreateDTO> ParseAsync(DbContext ctx, long id);
}
