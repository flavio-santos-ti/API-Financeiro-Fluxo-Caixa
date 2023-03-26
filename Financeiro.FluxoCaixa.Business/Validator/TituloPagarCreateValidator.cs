﻿using Financeiro.FluxoCaixa.Domain.Dtos.TituloPagar;
using FluentValidation;

namespace Financeiro.FluxoCaixa.Business.Validator;

public class TituloPagarCreateValidator : AbstractValidator<TituloPagarCreateDto>
{
    public TituloPagarCreateValidator()
    {
        RuleFor(x => x.CategoriaId).NotNull().GreaterThan(0).WithMessage("O Id da Categoria deve ser informado!");
        RuleFor(x => x.Descricao).NotNull().NotEmpty().NotEqual("string").WithMessage("A Descrição deve ser informada!");
        RuleFor(x => x.ValorReal).NotNull().GreaterThan(0).WithMessage("O Valor Real deve ser informado!");
        RuleFor(x => x.DataReal).NotNull().NotEmpty().WithMessage("A Data Real deve ser informada!");
        RuleFor(x => x.FornecedorId).NotNull().GreaterThan(0).WithMessage("O Id da Fornecedor deve ser informado!");
    }
}
