using MediatR;

namespace ModuloClientes.Core.Ports.Commands.ClienteCommands
{
    public record EliminarOficioCommand(
        Guid ClienteId,
        string Oficio
    ):IRequest<IEnumerable<string>>;
}