using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Domain.Dtos.Fornecedor;

public class FornecedorDto
{
    public long Id { get; set; }
    public string Nome { get; set; }
    public DateTime DataInclusao { get; set; }
}
