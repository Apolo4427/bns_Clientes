namespace ModuloClientes.Core.Ports.Commands.ClienteCommands
{
    public interface IUpdateOficiosCommandHandler :
    ICommandHandler<UpdateOficiosCommand,IEnumerable<string>>
    { }
}