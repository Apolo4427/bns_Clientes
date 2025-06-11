using MediatR;
using ModuloClientes.Core.Enums;

namespace ModuloClientes.Aplication.Command.ClienteCommands
{
    public record AddClienteRelacionCommand(
        Guid ClienteId,
        Guid RelacionadoId,
        TipoRelacion Tipo,
        bool EsDependiente
    ) : IRequest;
}