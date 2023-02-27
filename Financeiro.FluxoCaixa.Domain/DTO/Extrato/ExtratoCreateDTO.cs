using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Domain.DTO.Extrato;

public class ExtratoCreateDTO
{
    public string Descricao { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataExtrato { get; set; }
}
