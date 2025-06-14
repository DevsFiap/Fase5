namespace Fase5.Application.Dtos.Pedido.Request;

public record AtualizarStatusPedidoRequest(bool Aceitar, string? Motivo);