﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Domain.Dtos.TituloReceber;

public class TituloReceberDto
{
    public long Id { get; set; }
    public long ClienteId { get; set; }
    public string Origem { get; set; }
    public string Descricao { get; set; }
    public decimal ValorReal { get; set; }
    public DateTime DataReal { get; set; }
    public DateTime DataInclusao { get; set; }
}
