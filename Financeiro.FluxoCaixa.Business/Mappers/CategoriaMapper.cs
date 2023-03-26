using Financeiro.FluxoCaixa.Domain.Dtos.Categoria;
using Financeiro.FluxoCaixa.Domain.Entities;

namespace Financeiro.FluxoCaixa.Business.Mappers;

public class CategoriaMapper : AutoMapper.Profile
{
    public CategoriaMapper()
    {
        CreateMap<Categoria, CategoriaCreateDto>().ReverseMap();
        CreateMap<Categoria, CategoriaDto>().ReverseMap();
    }
}
