using MediatR;
using ModuloClientes.Core.Enums;

namespace ModuloClientes.Aplication.Command.ClienteCommands
{
    public record CambiarEstadoClienteCommand(
        Guid Id
    ) : IRequest<EstadoCliente>;
}