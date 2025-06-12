using Fase5.Domain.Entities;
using FluentValidation;

namespace Fase5.Domain.Validations;

public class ProdutoValidator : AbstractValidator<Produto>
{
    public ProdutoValidator()
    {
        RuleFor(p => p.Nome)
            .NotEmpty().MaximumLength(80);

        RuleFor(p => p.Descricao)
            .MaximumLength(255);

        RuleFor(p => p.Preco)
            .GreaterThan(0).WithMessage("Preço deve ser maior que zero.");

        RuleFor(p => p.Categoria)
            .IsInEnum();
    }
}