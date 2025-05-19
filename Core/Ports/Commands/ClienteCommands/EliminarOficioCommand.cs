namespace ModuloClientes.Core.Ports.Commands.ClienteCommands
{
    public record EliminarOficioCommand(
        int clienteId,
        string oficio
    );
}