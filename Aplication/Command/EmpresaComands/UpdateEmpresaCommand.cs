using MediatR;

namespace ModuloClientes.Aplication.Command.EmpresaCommands
{
    public record UpdateEmpresaCommand(
        Guid Id,
        byte[] RowVersion,
        string? Nombre = null,
        string? EIN = null,
        string? Direccion = null,
        string? Telefono = null,
        string? CorreoContacto = null,
        DateTime? FechaConstitucion = null
    ):IRequest;
}