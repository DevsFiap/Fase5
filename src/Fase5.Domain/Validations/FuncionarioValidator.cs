using Fase5.Domain.Entities;
using Fase5.Domain.Enuns;
using FluentValidation;

namespace Fase5.Domain.Validations;

public class FuncionarioValidator : AbstractValidator<Funcionario>
{
    public FuncionarioValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(f => f.Nome)
            .NotEmpty().MaximumLength(80);

        RuleFor(f => f.Email)
            .NotEmpty().EmailAddress().MaximumLength(80);

        RuleFor(f => f.Senha)
            .NotEmpty().MinimumLength(6).MaximumLength(128);

        RuleFor(f => f.Cargo)
            .IsInEnum().NotEqual(CargoFuncionario.Gerente).When(f => f.Cargo == 0)
            .WithMessage("Cargo inválido.");
    }
}