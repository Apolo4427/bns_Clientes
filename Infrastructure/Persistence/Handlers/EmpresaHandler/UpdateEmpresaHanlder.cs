using System.Data;
using ModuloClientes.Core.Models.ValueObjects.ClienteValueObjects;
using ModuloClientes.Core.Models.ValueObjects.EmpresaValueObjects;
using ModuloClientes.Core.Ports.Commands.EmpresaCommands;
using ModuloClientes.Core.Ports.Repositories;

namespace ModuloClientes.Infrastructure.Persistence.Handlers.EmpresaHandler
{
    public class UpdateEmpresaHandler : IUpdateEmpresaCommandHandler
    {

        private readonly IEmpresaRepository _repository;

        public UpdateEmpresaHandler(IEmpresaRepository repository)
        {
            _repository = repository;
        }
        public async Task HandleAsync(UpdateEmpresaCommand command)
        {
            var empresa = await _repository.GetByIdAsync(command.Id)
                ?? throw new ArgumentException($"La empresa con Id: {command.Id} no se ha encontrado");

            if (!empresa.RowVersion.SequenceEqual(command.RowVersion))
                throw new DBConcurrencyException("El registro ha sido modificado por otro usuario. Por favor refresque los datos.");

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
            
            await _repository.UpdateAsync(empresa);
        }
    }
}