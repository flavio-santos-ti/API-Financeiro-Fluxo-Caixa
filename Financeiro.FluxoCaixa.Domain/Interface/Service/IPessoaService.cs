using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Financeiro.FluxoCaixa.Domain.Dtos.Result;
using Financeiro.FluxoCaixa.Domain.Dtos.Pessoa;

namespace Financeiro.FluxoCaixa.Domain.Interface.Service;

public interface IPessoaService
{
    Task<ResultCreateDto> SetIncluirAsync(DbContext ctx, PessoaCreateDto dados);
}
