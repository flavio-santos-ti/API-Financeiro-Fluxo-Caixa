using Financeiro.FluxoCaixa.Domain.DTO.Cliente;
using Financeiro.FluxoCaixa.Domain.DTO.Pessoa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Business.Profile;

public class ClienteProfile : AutoMapper.Profile
{
    public ClienteProfile()
    {
       CreateMap<ClienteCreateDTO, PessoaCreateDTO> ().ReverseMap();
    }
}
