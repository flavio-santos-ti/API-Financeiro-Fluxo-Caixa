using Financeiro.FluxoCaixa.Domain.DTO.Fornecedor;
using Financeiro.FluxoCaixa.Domain.DTO.Pessoa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Business.Profile;

public class FornecedorProfile : AutoMapper.Profile
{
    public FornecedorProfile()
    {
        CreateMap<FornecedorCreateDTO, PessoaCreateDTO>().ReverseMap();
    }
}
