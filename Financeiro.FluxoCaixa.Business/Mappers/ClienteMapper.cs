using Financeiro.FluxoCaixa.Domain.Dtos.Cliente;
using Financeiro.FluxoCaixa.Domain.Dtos.Pessoa;

namespace Financeiro.FluxoCaixa.Business.Mappers;

public class ClienteMapper : AutoMapper.Profile
{
    public ClienteMapper()
    {
       CreateMap<ClienteCreateDto, PessoaCreateDto> ().ReverseMap();
    }
}
