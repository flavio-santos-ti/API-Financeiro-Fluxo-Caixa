using Financeiro.FluxoCaixa.Domain.Dtos.Fornecedor;
using Financeiro.FluxoCaixa.Domain.Dtos.Pessoa;

namespace Financeiro.FluxoCaixa.Business.Mappers;

public class FornecedorMapper : AutoMapper.Profile
{
    public FornecedorMapper()
    {
        CreateMap<FornecedorCreateDto, PessoaCreateDto>().ReverseMap();
    }
}
