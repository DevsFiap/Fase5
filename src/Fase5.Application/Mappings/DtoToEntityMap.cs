using AutoMapper;
using Fase5.Application.Dtos.Medicos.Request;
using Fase5.Application.Dtos.Medicos.Response;
using Fase5.Application.Dtos.Pacientes.Request;
using Fase5.Application.Dtos.Pacientes.Response;
using Fase5.Domain.Entities;

namespace Fase5.Application.Mappings;

public class DtoToEntityMap : Profile
{
    public DtoToEntityMap()
    {
        #region Médico
        CreateMap<CreateMedicoRequest, Medico>()
            .ForMember(d => d.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));

        CreateMap<UpdateMedicoRequest, Medico>()
            .ForAllMembers(opt => opt.Condition((src, _, srcMember) => srcMember is not null));

        CreateMap<Medico, MedicoDetailResponse>();
        CreateMap<Medico, MedicoListItemResponse>();
        #endregion

        #region Paciente
        CreateMap<CreatePacienteRequest, Paciente>()
            .ForMember(d => d.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));

        CreateMap<UpdatePacienteRequest, Paciente>()
            .ForAllMembers(opt => opt.Condition((src, _, val) => val is not null));

        CreateMap<Paciente, PacienteResponse>();
        #endregion
    }
}