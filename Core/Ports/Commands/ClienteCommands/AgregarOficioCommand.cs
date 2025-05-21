namespace ModuloClientes.Core.Ports.Commands.ClienteCommands
{
    public record AgregarOficioCommand(
        Guid ClienteId,
        string Oficio
    );
}