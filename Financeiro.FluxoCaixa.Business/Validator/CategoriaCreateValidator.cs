using Financeiro.FluxoCaixa.Domain.Dtos.Categoria;
using FluentValidation;

namespace Financeiro.FluxoCaixa.Business.Validator;

public class CategoriaCreateValidator : AbstractValidator<CategoriaCreateDto>
{
    public CategoriaCreateValidator()
    {
        RuleFor(x => x.Nome).NotNull().NotEmpty().NotEqual("string").WithMessage("A nome deve ser informada!");
        RuleFor(x => x.Tipo).NotNull().NotEmpty().NotEqual("string").WithMessage("O tipo deve ser informada!");
        RuleFor(x => x.Tipo).NotNull().NotEmpty().Length(1).WithMessage("O tipo deve ser E ou S");
    }
}
