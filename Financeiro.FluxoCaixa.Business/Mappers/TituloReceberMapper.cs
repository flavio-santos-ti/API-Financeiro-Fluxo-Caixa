using Financeiro.FluxoCaixa.Domain.Dtos.TituloReceber;
using Financeiro.FluxoCaixa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Business.Mappers;

public class TituloReceberMapper : AutoMapper.Profile
{
    public TituloReceberMapper()
    {
        CreateMap<TituloReceber, TituloReceberCreateDto>().ReverseMap();
    }
}
