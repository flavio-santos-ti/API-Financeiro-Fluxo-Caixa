﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Domain.Entity;

public class Cliente
{
    public long Id { get; set; }
    public long PessoaId { get; set; }
    public DateTime DataInclusao { get; set; }
}
