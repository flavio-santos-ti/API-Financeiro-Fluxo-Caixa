using Financeiro.FluxoCaixa.Domain.Entities.Base;

namespace Financeiro.FluxoCaixa.Domain.Entities;

public class Categoria : Entity
{
    public string Nome { get; set; }
    public string Tipo { get; set; }
}
