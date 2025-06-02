using MediatR;

namespace ModuloClientes.Aplication.Command.ClienteCommands
{
    public record EliminarOficioCommand(
        Guid ClienteId,
        string Oficio
    ):IRequest<IEnumerable<string>>;
}