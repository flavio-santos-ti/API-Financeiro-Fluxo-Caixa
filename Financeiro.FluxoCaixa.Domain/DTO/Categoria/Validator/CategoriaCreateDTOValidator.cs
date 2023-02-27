using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financeiro.FluxoCaixa.Domain.DTO.Categoria.Validator;

public class CategoriaCreateDTOValidator : AbstractValidator<CategoriaCreateDTO>
{
    public CategoriaCreateDTOValidator()
    {
        RuleFor(x => x.Nome).NotNull().NotEmpty().NotEqual("string").WithMessage("A nome deve ser informada!");
        RuleFor(x => x.Tipo).NotNull().NotEmpty().NotEqual("string").WithMessage("O tipo deve ser informada!");
        RuleFor(x => x.Tipo).NotNull().NotEmpty().Length(1).WithMessage("O tipo deve ser E ou S");
    }
}
