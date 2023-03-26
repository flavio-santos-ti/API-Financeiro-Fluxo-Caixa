using Financeiro.FluxoCaixa.Domain.Dtos.Fornecedor;
using Financeiro.FluxoCaixa.Domain.Dtos.Pessoa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Business.Mappers;

public class FornecedorMapper : AutoMapper.Profile
{
    public FornecedorMapper()
    {
        CreateMap<FornecedorCreateDto, PessoaCreateDto>().ReverseMap();
    }
}
