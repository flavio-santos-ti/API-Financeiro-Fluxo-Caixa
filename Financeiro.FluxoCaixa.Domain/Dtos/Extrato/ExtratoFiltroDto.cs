using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Domain.Dtos.Extrato;

public class ExtratoFiltroDto
{
    public DateTime DataInicial { get; set; }
    public DateTime DataFinal { get; set; }
}
