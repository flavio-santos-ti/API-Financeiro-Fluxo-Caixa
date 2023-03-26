using Financeiro.FluxoCaixa.Domain.Entities.Base;

namespace Financeiro.FluxoCaixa.Domain.Entities;

public class TituloPagar : Entity
{
    public int CategoriaId { get; set; }
    public long FornecedorId { get; set; }
    public string Descricao { get; set; }
    public decimal ValorReal { get; set; }
    public DateTime DataReal { get; set; }
    public DateTime DataInclusao { get; set; }
    public long ExtratoId { get; set; }
}
