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

            cliente.AgregarOficio(command.Oficio);

            await _repo.UpdateAsync(cliente);

            return cliente.Oficios;
        }
    }
}