using MediatR;

namespace ModuloClientes.Aplication.Command.ClienteCommands
{
    public record UpdateOficiosCommand(
        Guid ClienteId,
        IEnumerable<string> Oficios
    ):IRequest<IEnumerable<string>>;
}