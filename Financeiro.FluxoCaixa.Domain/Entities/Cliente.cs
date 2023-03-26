using Financeiro.FluxoCaixa.Domain.Entities.Base;

namespace Financeiro.FluxoCaixa.Domain.Entities;

public class Cliente : Entity
{
    public long PessoaId { get; set; }
    public DateTime DataInclusao { get; set; }
}
