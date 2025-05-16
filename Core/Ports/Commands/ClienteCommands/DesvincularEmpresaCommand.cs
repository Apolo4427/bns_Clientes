namespace ModuloClientes.Core.Ports.Commands.ClienteCommands
{
    public record DesvincularEmpresaCommand(
        int ClienteId,
        int EmpresaId
    );
}