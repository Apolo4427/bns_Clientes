using MediatR;

namespace ModuloClientes.Core.Ports.Commands.ClienteCommands
{
    public record UpdateOficiosCommand(
        Guid ClienteId,
        IEnumerable<string> Oficios
    ):IRequest<IEnumerable<string>>;
}