using Financeiro.FluxoCaixa.Domain.DTO.Fornecedor;
using Financeiro.FluxoCaixa.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Domain.Interface.Repository;

public interface IFornecedorRepository
{
    Task<Fornecedor> GetNoTrackingAsync(DbContext ctx, long idFornecedor);
    Task<IEnumerable<FornecedorDTO>> GetListarAsync();
}
