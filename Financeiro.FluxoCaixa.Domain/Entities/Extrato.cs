using Financeiro.FluxoCaixa.Domain.Entities.Base;

namespace Financeiro.FluxoCaixa.Domain.Entities;

public class Extrato : Entity
{
    public string Tipo { get; set; }
    public string Descricao { get; set; }
    public decimal Valor { get; set; }
    public decimal Saldo { get; set; }
    public decimal ValorRelatorio { get; set; }
    public DateTime DataExtrato { get; set; }
    public DateTime DataInclusao { get; set; }
}
