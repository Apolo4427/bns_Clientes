using MediatR;

namespace ModuloClientes.Core.Ports.Commands.EmpresaCommands
{
    public record CreateEmpresaCommand(
        string Nombre,
        string EIN,
        string Direccion,
        string Telefono,
        string CorreoContacto,
        DateTime FechaConstitucion
    ): IRequest<Guid>;
}