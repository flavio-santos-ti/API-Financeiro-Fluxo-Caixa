using Financeiro.FluxoCaixa.Domain.Dtos.Cliente;
using Financeiro.FluxoCaixa.Domain.Dtos.Pessoa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Business.Mappers;

public class ClienteMapper : AutoMapper.Profile
{
    public ClienteMapper()
    {
       CreateMap<ClienteCreateDto, PessoaCreateDto> ().ReverseMap();
    }
}
