namespace ModuloClientes.Core.Ports.Commands.EmpresaCommands
{
    public record UpdateEmpresaCommand(
        int Id,
        string? Nombre = null,
        string? EIN = null,
        string? Direccion = null,
        string? Telefono = null,
        string? CorreoContacto = null,
        DateTime? FechaConstitucion = null
    );
}