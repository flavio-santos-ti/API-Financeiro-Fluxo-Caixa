using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Domain.DTO.TituloReceber;

public class TituloReceberCreateDTO
{
    public int CategoriaId { get; set; }
    public string Descricao { get; set; }
    public decimal ValorReal { get; set; }
    public DateTime DataReal { get; set; }
    public long ClienteId { get; set; }
}
