using MediatR;

namespace ModuloClientes.Core.Ports.Commands.EmpresaCommands
{
    public record DeleteEmpresaCommand(Guid Id):IRequest;
}