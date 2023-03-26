using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Domain.Dtos.Result;

public class ResultDeleteDto
{
    public bool Successed { get; set; }
    public string Message { get; set; }
    public int Count { get; set; }
}
