﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Domain.DTO.Result;

public class ResultDTO
{
    public bool Successed { get; set; }
    public string Message { get; set; }
    public int Count { get; set; }
}
