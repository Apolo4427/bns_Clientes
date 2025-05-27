using MediatR;

namespace ModuloClientes.Core.Ports.Commands.ClienteCommands
{
    public record DesvincularEmpresaCommand(
        Guid ClienteId,
        Guid EmpresaId
    ): IRequest;
}