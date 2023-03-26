using Financeiro.FluxoCaixa.Domain.Dtos.TituloReceber;
using Financeiro.FluxoCaixa.Domain.Entities;

namespace Financeiro.FluxoCaixa.Business.Mappers;

public class TituloReceberMapper : AutoMapper.Profile
{
    public TituloReceberMapper()
    {
        CreateMap<TituloReceber, TituloReceberCreateDto>().ReverseMap();
    }
}
