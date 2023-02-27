using Financeiro.FluxoCaixa.Domain.DTO.TituloReceber;
using Financeiro.FluxoCaixa.Domain.Entity;
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
        CreateMap<TituloReceber, TituloReceberCreateDTO>().ReverseMap();
    }
}
