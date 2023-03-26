using Financeiro.FluxoCaixa.Domain.Dtos.TituloPagar;
using Financeiro.FluxoCaixa.Domain.Entities;

namespace Financeiro.FluxoCaixa.Business.Mappers;

public class TituloPagarMapper : AutoMapper.Profile
{
    public TituloPagarMapper()
    {
        CreateMap<TituloPagar, TituloPagarCreateDto>().ReverseMap();
    }
}
