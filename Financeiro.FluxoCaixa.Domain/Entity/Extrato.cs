using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Domain.Entity;

public class Extrato
{
    public long Id { get; set; }
    public string Tipo { get; set; }
    public string Descricao { get; set; }
    public decimal Valor { get; set; }
    public decimal Saldo { get; set; }
    public decimal ValorRelatorio { get; set; }
    public DateTime DataExtrato { get; set; }
    public DateTime DataInclusao { get; set; }
}
