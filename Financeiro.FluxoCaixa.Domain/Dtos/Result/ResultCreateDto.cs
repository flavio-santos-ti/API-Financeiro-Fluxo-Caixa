using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Domain.Dtos.Result;

public class ResultCreateDto
{
    public bool Successed { get; set; }
    public string Message { get; set; }
    public long Id { get; set; }
}
