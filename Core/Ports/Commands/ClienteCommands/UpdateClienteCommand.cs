using ModuloClientes.Core.Enums;

namespace ModuloClientes.Core.Ports.Commands.ClienteCommands
{
     /// <summary>
    /// Comando para actualizar datos de un cliente existente.
    /// Propiedades nulas no se modificarán.
    /// </summary>
    public record UpdateClienteCommand(
        Guid Id,
        byte[] RowVersion,
        string? Nombre = null,
        string? Apellido = null,
        string? Correo = null,
        string? Telefono = null,
        DateTime? FechaNacimiento = null,
        MaritalStatus? EstadoCivil = null,
        TaxStatus? EstadoTributario = null,
        string? SocialSecurityNumber = null,
        string? Direccion = null  
    );
}