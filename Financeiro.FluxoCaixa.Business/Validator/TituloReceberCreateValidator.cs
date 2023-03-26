using Financeiro.FluxoCaixa.Domain.Dtos.TituloReceber;
using FluentValidation;

namespace Financeiro.FluxoCaixa.Business.Validator;

public class TituloReceberCreateValidator : AbstractValidator<TituloReceberCreateDto>
{
    public TituloReceberCreateValidator()
    {
        RuleFor(x => x.CategoriaId).NotNull().GreaterThan(0).WithMessage("O Id da Categoria deve ser informado!");
        RuleFor(x => x.Descricao).NotNull().NotEmpty().NotEqual("string").WithMessage("A Descrição deve ser informada!");
        RuleFor(x => x.ValorReal).NotNull().GreaterThan(0).WithMessage("O Valor Real deve ser informado!");
        RuleFor(x => x.DataReal).NotNull().NotEmpty().WithMessage("A Data Real deve ser informada!");
        RuleFor(x => x.ClienteId).NotNull().GreaterThan(0).WithMessage("O Id do Cliente deve ser informado!");
    }
}
