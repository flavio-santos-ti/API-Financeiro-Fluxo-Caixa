using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Domain.Dtos.TituloPagar;

public class TituloPagarCreateDto
{
    public int CategoriaId { get; set; }
    public long FornecedorId { get; set; }
    public string Descricao { get; set; }
    public decimal ValorReal { get; set; }
    public DateTime DataReal { get; set; }
}
