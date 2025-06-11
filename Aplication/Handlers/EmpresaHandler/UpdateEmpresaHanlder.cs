using MediatR;
using Microsoft.EntityFrameworkCore;
using ModuloClientes.Aplication.Command.EmpresaCommands;
using ModuloClientes.Core.Models.ValueObjects.ClienteValueObjects;
using ModuloClientes.Core.Models.ValueObjects.EmpresaValueObjects;
using ModuloClientes.Core.Ports.IRepositories;

namespace ModuloClientes.Aplication.Handlers.EmpresaHandler
{
    public class UpdateEmpresaHandler : IRequestHandler<UpdateEmpresaCommand>
    {

        private readonly IEmpresaRepository _repository;

        public UpdateEmpresaHandler(IEmpresaRepository repository)
        {
            _repository = repository;
        }
        public async Task Handle(UpdateEmpresaCommand command, CancellationToken ct)
        {
            var empresa = await _repository.GetByIdAsync(command.Id)
                ?? throw new ArgumentException($"La empresa con Id: {command.Id} no se ha encontrado");

            if (!empresa.RowVersion.SequenceEqual(command.RowVersion))
                throw new DbUpdateConcurrencyException($"La empresa de id {command.Id} ha sido modificado por otro usuario. Por favor refresque los datos.");

            if (!string.IsNullOrWhiteSpace(command.Nombre))
                empresa.CambiarNombre(new CompanyName(command.Nombre));
            if (!string.IsNullOrWhiteSpace(command.EIN))
                empresa.CambiarEIN(new EIN(command.EIN));
            if (!string.IsNullOrWhiteSpace(command.Direccion))
                empresa.CambiarDireccion(new Address(command.Direccion));
            if (!string.IsNullOrWhiteSpace(command.Telefono))
                empresa.CambiarTelefono(new Phone(command.Telefono));
            if (!string.IsNullOrWhiteSpace(command.CorreoContacto))
                empresa.CambiarCorreoContacto(new Email(command.CorreoContacto));
            if (command.FechaConstitucion.HasValue)
                empresa.CambiarFechaConstitucion(command.FechaConstitucion.Value);

            try
            {
                await _repository.UpdateAsync(empresa, ct);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new DbUpdateConcurrencyException(
                    $"La empresa de id {command.Id} ya ha sido modificada en otro proceso"
                );
            }
        }
    }
}