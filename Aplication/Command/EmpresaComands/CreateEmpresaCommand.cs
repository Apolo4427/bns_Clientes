using MediatR;

namespace ModuloClientes.Aplication.Command.EmpresaCommands
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