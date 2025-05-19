namespace ModuloClientes.Core.Ports.Commands.ClienteCommands
{
    public interface IEliminarOficioCommandHandler :
    ICommandHandler<EliminarOficioCommand,IEnumerable<string>>
    { }
}