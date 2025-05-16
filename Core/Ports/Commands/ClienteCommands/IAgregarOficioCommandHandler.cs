namespace ModuloClientes.Core.Ports.Commands.ClienteCommands
{
    public interface IAgregarOficioCommandHandler : ICommandHandler<AgregarOficioCommand,
         IEnumerable<string>>{}
}