using Financeiro.FluxoCaixa.Domain.DTO.Categoria;
using Financeiro.FluxoCaixa.Domain.DTO.Result;
using Financeiro.FluxoCaixa.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Domain.Interface.Service;

public interface ICategoriaService
{
    Task<ResultCreateDTO> SetCadastrarAsync(CategoriaCreateDTO dados);
    Task<ResultDeleteDTO> SetRemoverAsync(int id);
    Task<IEnumerable<CategoriaDTO>> GetListarAsync();
    Task<ResultCreateDTO> ParseAsync(DbContext ctx, int id);
}
