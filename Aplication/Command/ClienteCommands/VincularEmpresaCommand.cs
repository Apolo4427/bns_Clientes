using MediatR;

namespace ModuloClientes.Aplication.Command.ClienteCommands
{
    public record VincularEmpresaCommand(
        Guid ClienteId,
        Guid EmpresaId,
        string Rol,
        DateTime FechaVinculacion
    ): IRequest;
}