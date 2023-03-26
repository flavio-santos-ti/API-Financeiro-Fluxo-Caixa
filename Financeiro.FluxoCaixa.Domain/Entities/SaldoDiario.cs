using Financeiro.FluxoCaixa.Domain.Entities.Base;

namespace Financeiro.FluxoCaixa.Domain.Entities;

public class SaldoDiario : Entity
{
    public DateTime DataSaldo { get; set; }
    public string Tipo { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataInclusao { get; set; }
    public long ExtratoId { get; set; }
}
