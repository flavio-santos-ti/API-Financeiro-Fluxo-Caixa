using Financeiro.FluxoCaixa.Domain.Entities.Base;

namespace Financeiro.FluxoCaixa.Domain.Entities;

public class Pessoa : Entity
{
    public string Nome { get; set; }
    public string HashNome { get; set; }
    public DateTime DataInclusao { get; set; }
}
