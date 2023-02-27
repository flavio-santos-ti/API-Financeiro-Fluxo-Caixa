using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Financeiro.FluxoCaixa.Domain.DTO.Result;
using Financeiro.FluxoCaixa.Domain.DTO.Pessoa;

namespace Financeiro.FluxoCaixa.Domain.Interface.Service;

public interface IPessoaService
{
    Task<ResultCreateDTO> SetIncluirAsync(DbContext ctx, PessoaCreateDTO dados);
}
