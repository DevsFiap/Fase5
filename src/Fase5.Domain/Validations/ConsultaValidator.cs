using Fase5.Domain.Entities;
using FluentValidation;

namespace Fase5.Domain.Validations;

public class ConsultaValidator : AbstractValidator<Consulta>
{
    public ConsultaValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(c => c.MedicoId)
            .NotEmpty().WithMessage("MedicoId obrigatório.");

        RuleFor(c => c.PacienteId)
            .NotEmpty().WithMessage("PacienteId obrigatório.");

        RuleFor(c => c.DataHora)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("Data da consulta deve ser futura.");

        RuleFor(c => c.Valor)
            .GreaterThan(0).WithMessage("Valor da consulta deve ser maior que zero.");

        RuleFor(c => c.Status)
            .IsInEnum();
    }
}