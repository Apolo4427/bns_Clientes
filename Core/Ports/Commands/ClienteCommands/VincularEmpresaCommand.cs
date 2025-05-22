namespace ModuloClientes.Core.Ports.Commands.ClienteCommands
{
    public record VincularEmpresaCommand(
        Guid ClienteId,
        Guid EmpresaId,
        string Rol,
        DateTime FechaVinculacion
    );
}