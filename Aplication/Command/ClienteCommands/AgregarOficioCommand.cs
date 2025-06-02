using MediatR;

namespace ModuloClientes.Aplication.Command.ClienteCommands
{
    public record AgregarOficioCommand(
        Guid ClienteId,
        string Oficio
    ):IRequest<IEnumerable<string>>;
}