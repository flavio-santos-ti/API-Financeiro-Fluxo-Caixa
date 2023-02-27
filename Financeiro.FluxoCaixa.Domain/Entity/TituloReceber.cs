using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Domain.Entity;

public class TituloReceber
{
    public long Id { get; set; }
    public int CategoriaId { get; set; }
    public string Descricao { get; set; }
    public decimal ValorReal { get; set; }
    public DateTime DataReal { get; set; }
    public long ClienteId { get; set; }
    public DateTime DataInclusao { get; set; }
    public long ExtratoId { get; set; }
}
