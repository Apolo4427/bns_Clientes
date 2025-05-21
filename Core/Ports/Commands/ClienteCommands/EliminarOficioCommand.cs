namespace ModuloClientes.Core.Ports.Commands.ClienteCommands
{
    public record EliminarOficioCommand(
        int ClienteId,
        string Oficio
    );
}