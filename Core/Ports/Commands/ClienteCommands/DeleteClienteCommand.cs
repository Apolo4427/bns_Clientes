using MediatR;

namespace ModuloClientes.Core.Ports.Commands.ClienteCommands
{
    /// <summary>
    /// Comando para eliminar un cliente por su identificador.
    /// </summary>
    public record DeleteClienteCommand(Guid Id):IRequest;
}