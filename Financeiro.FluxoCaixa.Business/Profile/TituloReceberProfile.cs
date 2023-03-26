using Financeiro.FluxoCaixa.Domain.Dtos.TituloReceber;
using Financeiro.FluxoCaixa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Business.Profile;

public class TituloReceberProfile : AutoMapper.Profile
{
    public TituloReceberProfile()
    {
        CreateMap<TituloReceber, TituloReceberCreateDto>().ReverseMap();
    }
}
