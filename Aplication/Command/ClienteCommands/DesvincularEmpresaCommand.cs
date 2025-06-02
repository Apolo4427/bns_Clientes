using MediatR;

namespace ModuloClientes.Aplication.Command.ClienteCommands
{
    public record DesvincularEmpresaCommand(
        Guid ClienteId,
        Guid EmpresaId
    ): IRequest;
}