using MediatR;

namespace ModuloClientes.Aplication.Command.ClienteCommands
{
    /// <summary>
    /// Comando para crear un nuevo cliente.
    /// </summary>
    public record CreateClienteCommand(
        string Nombre,
        string Apellido,
        string Correo,
        string Telefono,
        DateTime FechaNacimiento,
        string EstadoCivil,
        string EstadoTributario,
        string SocialSecurityNumber,
        string Direccion
    ) : IRequest<Guid>;
}