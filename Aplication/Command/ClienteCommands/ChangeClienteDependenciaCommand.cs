using MediatR;

namespace ModuloClientes.Aplication.Command.ClienteCommands
{
    public record ChangeClienteDependenciaCommand(
        Guid ClienteId,
        Guid RelacionadoId,
        bool EsDependiente
    ) : IRequest;
}