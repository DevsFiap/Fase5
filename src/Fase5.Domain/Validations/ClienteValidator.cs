using Fase5.Domain.Entities;
using Fase5.Domain.Helpers;
using FluentValidation;

namespace Fase5.Domain.Validations;

public class ClienteValidator : AbstractValidator<Cliente>
{
    public ClienteValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(c => c.Nome)
            .NotEmpty().MaximumLength(80);

        RuleFor(c => c.Email)
            .NotEmpty().EmailAddress()
            .MaximumLength(80);

        RuleFor(c => c.Senha)
            .NotEmpty().MinimumLength(6).MaximumLength(128);

        RuleFor(c => c.CPF)
            .NotEmpty().Must(DocumentoHelper.CpfValido)
            .WithMessage("CPF inválido.");
    }
}