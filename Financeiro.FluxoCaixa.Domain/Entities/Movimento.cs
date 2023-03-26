using Financeiro.FluxoCaixa.Domain.Entities.Base;

namespace Financeiro.FluxoCaixa.Domain.Entities;

public class Movimento : Entity
{
    public bool Tipo { get;set; }
    public decimal Valor { get; set; }
    public DateTime DataInclusao { get; set; }
}
