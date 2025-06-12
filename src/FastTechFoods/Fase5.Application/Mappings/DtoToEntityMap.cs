using AutoMapper;
using Fase5.Application.Dtos.Cliente.Request;
using Fase5.Application.Dtos.Cliente.Response;
using Fase5.Application.Dtos.Funcionario.Request;
using Fase5.Application.Dtos.Funcionario.Response;
using Fase5.Application.Dtos.ItensPedido.Request;
using Fase5.Application.Dtos.ItensPedido.Response;
using Fase5.Application.Dtos.Login.Response;
using Fase5.Application.Dtos.Pedido.Request;
using Fase5.Application.Dtos.Pedido.Response;
using Fase5.Application.Dtos.Produto.Request;
using Fase5.Application.Dtos.Produto.Response;
using Fase5.Domain.Entities;

namespace Fase5.Application.Mappings;

/// <summary>
/// Mapeia DTOs ⇄ Entidades
/// </summary>
public class DtoToEntityMap : Profile
{
    public DtoToEntityMap()
    {
        //Cliente
        CreateMap<CreateClienteRequest, Cliente>();
        CreateMap<UpdateClienteRequest, Cliente>()
            .ForAllMembers(opt => opt.Condition((src, _, v) => v is not null));

        CreateMap<Cliente, ClienteResponse>();

        //Funcionario
        CreateMap<CreateFuncionarioRequest, Funcionario>();
        CreateMap<UpdateFuncionarioRequest, Funcionario>()
            .ForAllMembers(opt => opt.Condition((src, _, v) => v is not null));

        CreateMap<Funcionario, FuncionarioResponse>();

        //Produto
        CreateMap<CreateProdutoRequest, Produto>();
        CreateMap<UpdateProdutoRequest, Produto>()
            .ForAllMembers(opt => opt.Condition((src, _, v) => v is not null));

        CreateMap<Produto, ProdutoResponse>();

        //ItemPedido
        CreateMap<ItemPedidoRequest, ItemPedido>();

        CreateMap<ItemPedido, ItemPedidoResponse>()
            .ForMember(d => d.ProdutoNome, o => o.MapFrom(s => s.Produto.Nome));

        //Pedido
        CreateMap<CreatePedidoRequest, Pedido>()
            .ForMember(d => d.Itens, o => o.MapFrom(s => s.Itens));

        CreateMap<Pedido, PedidoResponse>()
            .ForMember(d => d.Total, o => o.MapFrom(s => s.Total));

         //Login (helper tupla → DTO)
        CreateMap<(int id, string nome, string role, string token), LoginResponse>()
            .ConstructUsing(src =>
                new LoginResponse(src.id, src.nome, src.role, src.token, DateTime.UtcNow));
    }
}