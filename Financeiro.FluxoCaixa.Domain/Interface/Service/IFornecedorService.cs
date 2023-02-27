using Financeiro.FluxoCaixa.Domain.DTO.Fornecedor;
using Financeiro.FluxoCaixa.Domain.DTO.Result;
using Financeiro.FluxoCaixa.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Domain.Interface.Service;

public interface IFornecedorService
{
    Task<ResultCreateDTO> SetCadastrarAsync(FornecedorCreateDTO dados);
    Task<IEnumerable<FornecedorDTO>> GetListarAsync();
    Task<ResultCreateDTO> ParseAsync(DbContext ctx, long id);
}
