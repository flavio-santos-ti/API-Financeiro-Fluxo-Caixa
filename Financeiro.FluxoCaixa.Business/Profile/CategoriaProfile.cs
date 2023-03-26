using Financeiro.FluxoCaixa.Domain.Dtos.Categoria;
using Financeiro.FluxoCaixa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Business.Profile;

public class CategoriaProfile : AutoMapper.Profile
{
    public CategoriaProfile()
    {
        CreateMap<Categoria, CategoriaCreateDto>().ReverseMap();
        CreateMap<Categoria, CategoriaDto>().ReverseMap();
    }
}
