﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Domain.Dtos.Categoria;

public class CategoriaDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Tipo { get; set; }
}
