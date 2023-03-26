using Financeiro.FluxoCaixa.Domain.Dtos.Cliente;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Domain.DTO.Cliente.Validator;

public class ClienteCreateDTOValidator : AbstractValidator<ClienteCreateDto>
{
    public ClienteCreateDTOValidator()
    {
        RuleFor(x => x.Nome).NotNull().NotEmpty().WithMessage("O nome é de preenchimento obrigatório");
    }
}
