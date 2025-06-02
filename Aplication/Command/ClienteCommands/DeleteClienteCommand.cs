using MediatR;

namespace ModuloClientes.Aplication.Command.ClienteCommands
{
    /// <summary>
    /// Comando para eliminar un cliente por su identificador.
    /// </summary>
    public record DeleteClienteCommand(Guid Id):IRequest;
}