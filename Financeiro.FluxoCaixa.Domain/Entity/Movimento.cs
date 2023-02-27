using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Domain.Entity;

public class Movimento
{
    public long Id { get; set; }
    public bool Tipo { get;set; }
    public decimal Valor { get; set; }
    public DateTime DataInclusao { get; set; }
}
