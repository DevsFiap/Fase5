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
        CreateMap<CreateMedicoRequest, Cliente>()
            .ForMember(d => d.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));

        CreateMap<UpdateMedicoRequest, Cliente>()
            .ForAllMembers(opt => opt.Condition((src, _, srcMember) => srcMember is not null));

        CreateMap<Cliente, MedicoDetailResponse>();
        CreateMap<Cliente, MedicoListItemResponse>();
        #endregion

        #region Paciente
        CreateMap<CreatePacienteRequest, Funcionario>()
            .ForMember(d => d.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));

        CreateMap<UpdatePacienteRequest, Funcionario>()
            .ForAllMembers(opt => opt.Condition((src, _, val) => val is not null));

        CreateMap<Funcionario, PacienteResponse>();
        #endregion
    }
}