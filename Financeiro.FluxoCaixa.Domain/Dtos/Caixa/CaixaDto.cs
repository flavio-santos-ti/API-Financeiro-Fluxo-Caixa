﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Domain.Dtos.Caixa;

public class CaixaDto
{
    public DateTime DataUltimoMovimento { get; set; }
    public decimal Saldo { get; set; }
    public string Situacao { get; set; }
    public long SaldoId { get; set; } 
    
}
