namespace ModuloClientes.Core.Ports.Commands.ClienteCommands
{
    public record AgregarOficioCommand(
        int ClienteId,
        string Oficio
    );
}