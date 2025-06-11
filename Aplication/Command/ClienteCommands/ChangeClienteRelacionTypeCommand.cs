using MediatR;
using ModuloClientes.Core.Enums;

namespace ModuloClientes.Aplication.Command.ClienteCommands
{
    public record ChangeClienteRelacionTypeCommand(
        Guid ClienteId,
        Guid RelacionadoId,
        TipoRelacion NuevoTipo
    ) : IRequest;
}