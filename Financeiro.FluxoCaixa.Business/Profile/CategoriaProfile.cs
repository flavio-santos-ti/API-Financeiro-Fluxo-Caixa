using Financeiro.FluxoCaixa.Domain.DTO.Categoria;
using Financeiro.FluxoCaixa.Domain.Entity;
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
        CreateMap<Categoria, CategoriaCreateDTO>().ReverseMap();
        CreateMap<Categoria, CategoriaDTO>().ReverseMap();
    }
}
