using MediatR;

namespace ModuloClientes.Aplication.Command.ClienteCommands
{
    public record DeleteClienteRelacionCommand(
        Guid ClienteId,
        Guid RelacionadoId
    ) : IRequest;
}