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
            var empresa = await _repository.GetByIdAsync(command.Id);

            if (!string.IsNullOrWhiteSpace(command.Nombre))
                empresa.CambiarNombre(command.Nombre);
            if (!string.IsNullOrWhiteSpace(command.EIN))
                empresa.CambiarEin(command.EIN);
            if (!string.IsNullOrWhiteSpace(command.Direccion))
                empresa.CambiarDireccion(command.Direccion);
            if (!string.IsNullOrWhiteSpace(command.Telefono))
                empresa.CambiarTelefono(command.Telefono);
            if (!string.IsNullOrWhiteSpace(command.CorreoContacto))
                empresa.CambiarCorreo(command.CorreoContacto);
            if (command.FechaConstitucion.HasValue)
                empresa.CambiarFechaDeConstitucion(command.FechaConstitucion.Value);
            
            await _repository.UpdateAsync(empresa);
        }
    }
}