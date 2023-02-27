using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Domain.Entity;

public class SaldoDiario
{
    public long Id { get; set; }
    public DateTime DataSaldo { get; set; }
    public string Tipo { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataInclusao { get; set; }
    public long ExtratoId { get; set; }
}
