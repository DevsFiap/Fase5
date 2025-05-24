using Fase5.Domain.Entities;
using Fase5.Domain.Helpers;
using FluentValidation;

namespace Fase5.Domain.Validations;

public class MedicoValidator : AbstractValidator<Medico>
{
    public MedicoValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        Include(new UsuarioValidator());

        RuleFor(m => m.CRM)
            .NotEmpty().WithMessage("CRM obrigatório.")
            .Must(DocumentoHelper.CrmValido).WithMessage("CRM inválido.");

        RuleFor(m => m.Especialidade)
            .NotEmpty().WithMessage("Especialidade obrigatória.")
            .MaximumLength(60);

        RuleFor(m => m.ValorConsultaPadrao)
            .GreaterThan(0).WithMessage("Valor da consulta deve ser maior que zero.");

        RuleForEach(m => m.HorariosDisponiveis)
            .SetValidator(new HorarioDisponivelValidator());
    }
}