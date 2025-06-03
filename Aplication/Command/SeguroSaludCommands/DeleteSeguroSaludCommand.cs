using MediatR;

namespace ModuloClientes.Aplication.Command.SeguroSaludCommands
{
    /// <summary>
    /// Comando para eliminar un SeguroSalud por su identificador.
    /// </summary>
    public record DeleteSeguroSaludCommand(Guid Id) : IRequest;
}
