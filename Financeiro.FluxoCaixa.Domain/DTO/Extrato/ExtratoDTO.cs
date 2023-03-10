using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Domain.DTO.Extrato;

public class ExtratoDTO
{
    public long Id { get; set; }
    public string Tipo { get; set; }
    public string Descricao { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataExtrato { get; set; }
    public long TituloId { get; set; }
    public string Nome { get; set; }
}
