using Financeiro.FluxoCaixa.Domain.Dtos.Cliente;
using FluentValidation;

namespace Financeiro.FluxoCaixa.Business.Validator;

public class ClienteCreateValidator : AbstractValidator<ClienteCreateDto>
{
    public ClienteCreateValidator()
    {
        RuleFor(x => x.Nome).NotNull().NotEmpty().WithMessage("O nome é de preenchimento obrigatório");
    }
}
