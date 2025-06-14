using Fase5.Domain.Entities;
using FluentValidation;

namespace Fase5.Domain.Validations;

public class PedidoValidator : AbstractValidator<Pedido>
{
    public PedidoValidator()
    {
        RuleFor(p => p.ClienteId).GreaterThan(0);

        RuleFor(p => p.Entrega)
            .IsInEnum();

        RuleForEach(p => p.Itens)
            .SetValidator(new ItemPedidoValidator());

        RuleFor(p => p.Itens)
            .NotEmpty().WithMessage("Pedido deve conter pelo menos um item.");
    }
}