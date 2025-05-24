using Fase5.Domain.Entities;
using FluentValidation;

namespace Fase5.Domain.Validations;

public class UsuarioValidator : AbstractValidator<Usuario>
{
    public UsuarioValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(u => u.Nome)
            .NotEmpty().WithMessage("Nome obrigatório.")
            .MaximumLength(80);

        RuleFor(u => u.Senha)
            .NotEmpty().WithMessage("Senha obrigatória.")
            .MinimumLength(6);

        RuleFor(u => u.Perfil)
            .IsInEnum();
    }
}