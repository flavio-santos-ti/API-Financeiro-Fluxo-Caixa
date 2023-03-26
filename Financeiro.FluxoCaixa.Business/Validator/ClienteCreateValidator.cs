using Financeiro.FluxoCaixa.Domain.Dtos.Cliente;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Business.Validator;

public class ClienteCreateValidator : AbstractValidator<ClienteCreateDto>
{
    public ClienteCreateValidator()
    {
        RuleFor(x => x.Nome).NotNull().NotEmpty().WithMessage("O nome é de preenchimento obrigatório");
    }
}
