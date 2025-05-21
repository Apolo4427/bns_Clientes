namespace ModuloClientes.Core.Ports.Commands.ClienteCommands
{
    /// <summary>
    /// Handler para el comando CreateClienteCommand.
    /// </summary>
    public interface ICreateClienteCommandHandler
        : ICommandHandler<CreateClienteCommand, int>
    {
        
    }
}