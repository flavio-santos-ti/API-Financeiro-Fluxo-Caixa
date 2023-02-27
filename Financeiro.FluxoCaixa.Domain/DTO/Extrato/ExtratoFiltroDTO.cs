using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Domain.DTO.Extrato;

public class ExtratoFiltroDTO
{
    public DateTime DataInicial { get; set; }
    public DateTime DataFinal { get; set; }
}
