using ModuloClientes.Core.Models.ValueObjects.ClienteValueObjects;
using ModuloClientes.Core.Ports.Commands.ClienteCommands;
using ModuloClientes.Core.Ports.Repositories;

namespace ModuloClientes.Infrastructure.Persistence.Handlers.ClienteHandler
{
    public class AgregarOficioHandler : IAgregarOficioCommandHandler
    {
        private readonly IClienteRepository _repo;

        public AgregarOficioHandler(IClienteRepository clienteRepository)
        {
            _repo = clienteRepository;
        }

        public async Task<IEnumerable<string>> HandleAsync(AgregarOficioCommand command)
        {
            var cliente = await _repo.GetByIdAsync(command.ClienteId);

            var nuevoOficio = new Oficio(command.Oficio);

            cliente.AgregarOficio(nuevoOficio);

            await _repo.UpdateAsync(cliente);

            var response = new List<string>();

            foreach (var o in cliente.Oficios)
            {
                response.Add(o.ToString());
            }

            return response;
        }
    }
}