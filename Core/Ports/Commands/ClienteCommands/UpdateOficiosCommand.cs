namespace ModuloClientes.Core.Ports.Commands.ClienteCommands
{
    public record UpdateOficiosCommand(
        int ClienteId,
        IEnumerable<string> Oficios
    );
}