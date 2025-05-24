using Fase5.Domain.Entities;
using Fase5.Domain.Helpers;
using FluentValidation;

namespace Fase5.Domain.Validations;

public class PacienteValidator : AbstractValidator<Paciente>
{
    public PacienteValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        Include(new UsuarioValidator());

        RuleFor(p => p.CPF)
            .NotEmpty().WithMessage("CPF obrigatório.")
            .Must(DocumentoHelper.CpfValido).WithMessage("CPF inválido.");

        RuleForEach(p => p.Consultas)
            .SetValidator(new ConsultaValidator());
    }
}