using System.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ModuloClientes.Aplication.Command.ClienteCommands;
using ModuloClientes.Core.Models.ValueObjects.ClienteValueObjects;
using ModuloClientes.Core.Ports.IRepositories;

namespace ModuloClientes.Aplication.Handlers.ClienteHandler
{
    public class UpdateClienteHandler : IRequestHandler<UpdateClienteCommand>
    {
        private readonly IClienteRepository _repo;
        public UpdateClienteHandler(IClienteRepository repository)
        {
            _repo = repository;
        }

        public async Task Handle(UpdateClienteCommand command, CancellationToken ct)
        {
            var cliente = await _repo.GetByIdAsync(command.Id, ct)
                ?? throw new ArgumentException($"No se encontro el cliente con el id {command.Id}");

            if (!cliente.RowVersion.SequenceEqual(command.RowVersion))
                throw new DBConcurrencyException($"El cliente de id {command.Id} ha sido modificado por otro usuario. Por favor refresque los datos.");

            if (!string.IsNullOrWhiteSpace(command.Nombre))
                cliente.CambiarNombre(new Name(command.Nombre));
            if (!string.IsNullOrWhiteSpace(command.Apellido))
                cliente.CambiarApellido(new Surname(command.Apellido));
            if (!string.IsNullOrWhiteSpace(command.Correo))
                cliente.CambiarCorreo(new Email(command.Correo));
            if (!string.IsNullOrWhiteSpace(command.Telefono))
                cliente.CambiarTelefono(new Phone(command.Telefono));
            if (command.FechaNacimiento.HasValue)
                cliente.CambiarFechaNacimiento(command.FechaNacimiento.Value);
            if (command.EstadoCivil.HasValue)
                cliente.CambiarEstadoCivil(command.EstadoCivil.Value);
            if (command.EstadoTributario.HasValue)
                cliente.CambiarEstadoTributario(command.EstadoTributario.Value);
            if (!string.IsNullOrWhiteSpace(command.SocialSecurityNumber))
                cliente.CambiarSSN(new SSN(command.SocialSecurityNumber));
            if (!string.IsNullOrWhiteSpace(command.Direccion))
                cliente.CambiarDireccion(new Address(command.Direccion));

            try
            {
                await _repo.UpdateAsync(cliente, ct);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new DbUpdateConcurrencyException(
                    $"El cliente de id {command.Id} ya ha sido modificado por otro proceso"
                );
            }
        }
    }
}