using Fase5.Domain.Entities;
using FluentValidation;

namespace Fase5.Domain.Validations;

public class ItemPedidoValidator : AbstractValidator<ItemPedido>
{
    public ItemPedidoValidator()
    {
        RuleFor(i => i.ProdutoId).GreaterThan(0);
        RuleFor(i => i.Quantidade).GreaterThan(0);
        RuleFor(i => i.PrecoUnitario).GreaterThan(0);
    }
}