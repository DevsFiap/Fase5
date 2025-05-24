using Fase5.Domain.Entities;
using FluentValidation;

namespace Fase5.Domain.Validations;

public class HorarioDisponivelValidator : AbstractValidator<HorarioDisponivel>
{
    public HorarioDisponivelValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(h => h.Inicio)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("Início deve estar no futuro.");

        RuleFor(h => h.Fim)
            .GreaterThan(h => h.Inicio)
            .WithMessage("Fim deve ser depois do início.")
            .Must((h, fim) => (fim - h.Inicio) >= TimeSpan.FromMinutes(15))
            .WithMessage("Duração mínima de 15 minutos.");

        RuleFor(h => h.MedicoId)
            .NotEmpty().WithMessage("MedicoId obrigatório.");
    }
}