using Financeiro.FluxoCaixa.Domain.Entities.Base;

namespace Financeiro.FluxoCaixa.Domain.Entities;

public class TituloReceber : Entity
{
    public int CategoriaId { get; set; }
    public string Descricao { get; set; }
    public decimal ValorReal { get; set; }
    public DateTime DataReal { get; set; }
    public long ClienteId { get; set; }
    public DateTime DataInclusao { get; set; }
    public long ExtratoId { get; set; }
}
