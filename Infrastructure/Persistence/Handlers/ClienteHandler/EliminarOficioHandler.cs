using ModuloClientes.Core.Ports.Commands;
using ModuloClientes.Core.Ports.Commands.ClienteCommands;
using ModuloClientes.Core.Ports.Repositories;

namespace ModuloClientes.Infrastructure.Persistence.Handlers.ClienteHandler
{
    public class EliminarOficioHandler : IEliminarOficioCommandHandler
    {
        private readonly IClienteRepository _repo;

        public EliminarOficioHandler(IClienteRepository clienteRepository)
        {
            _repo = clienteRepository;
        }
        public async Task<IEnumerable<string>> HandleAsync(EliminarOficioCommand command)
        {
            var cliente = await _repo.GetByIdAsync(command.ClienteId);

            cliente.EliminarOficio(command.Oficio);

            await _repo.UpdateAsync(cliente);

            return cliente.Oficios;
        }
    }
}