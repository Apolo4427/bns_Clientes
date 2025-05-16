using ModuloClientes.Core.Ports.Commands.ClienteCommands;
using ModuloClientes.Core.Ports.Repositories;

namespace ModuloClientes.Infrastructure.Persistence.Handlers.ClienteHandler
{
    public class UpdateClienteHandler : IUpdateClienteCommandHandler
    {

        private readonly IClienteRepository _repo;
        public UpdateClienteHandler(IClienteRepository repository)
        {
            _repo = repository;
        }

        public async Task HandleAsync(UpdateClienteCommand command)
        {
            var cliente = await _repo.GetByIdAsync(command.Id);

            if (!string.IsNullOrWhiteSpace(command.Nombre))
                cliente.CambiarNombre(command.Nombre);
            if (!string.IsNullOrWhiteSpace(command.Apellido))
                cliente.CambiarApellido(command.Apellido);
            if (!string.IsNullOrWhiteSpace(command.Correo))
                cliente.CambiarCorreo(command.Correo);
            if (!string.IsNullOrWhiteSpace(command.Telefono))
                cliente.CambiarTelefono(command.Telefono);
            if (command.FechaNacimiento.HasValue)
                cliente.CambiarFechaDeNacimiento(command.FechaNacimiento.Value);
            if (!string.IsNullOrWhiteSpace(command.EstadoCivil))
                cliente.CambiarEstadoCivil(command.EstadoCivil);
            if (!string.IsNullOrWhiteSpace(command.EstadoTributario))
                cliente.CambiarEstadoTributario(command.EstadoTributario);
            if (!string.IsNullOrWhiteSpace(command.SocialSecurityNumber))
                cliente.CambiarSocialSecurityNumber(command.SocialSecurityNumber);
            if (!string.IsNullOrWhiteSpace(command.Direccion))
                cliente.CambiarDireccion(command.Direccion);
            
            await _repo.UpdateAsync(cliente);
        }
    }
}