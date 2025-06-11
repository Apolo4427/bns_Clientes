using MediatR;

namespace ModuloClientes.Aplication.Command.EmpresaCommands
{
    public record DeleteEmpresaCommand(Guid Id):IRequest;
}