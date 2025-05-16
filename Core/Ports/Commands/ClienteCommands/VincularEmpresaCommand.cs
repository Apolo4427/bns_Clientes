namespace ModuloClientes.Core.Ports.Commands.ClienteCommands
{
    public record VincularEmpresaCommand(
        int ClienteId,
        int EmpresaId,
        string Rol,
        DateTime FechaVinculacion
    );
}