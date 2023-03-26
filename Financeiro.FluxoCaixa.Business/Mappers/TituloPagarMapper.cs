using Financeiro.FluxoCaixa.Domain.Dtos.TituloPagar;
using Financeiro.FluxoCaixa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Business.Mappers;

public class TituloPagarMapper : AutoMapper.Profile
{
    public TituloPagarMapper()
    {
        CreateMap<TituloPagar, TituloPagarCreateDto>().ReverseMap();
    }
}
