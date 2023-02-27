using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Domain.Entity;

public class Pessoa
{
    public long Id { get; set; }
    public string? Nome { get; set; }
    public string? HashNome { get; set; }
    public DateTime DataInclusao { get; set; }
}
