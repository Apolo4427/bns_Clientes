using ModuloClientes.Core.Models.ValueObjects.ClienteValueObjects;
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

            var oficioEliminado = new Oficio(command.Oficio);

            cliente.EliminarOficio(oficioEliminado);

            await _repo.UpdateAsync(cliente);

            var oficios = new List<string>();

            foreach (var o in cliente.Oficios)
            {
                oficios.Add(o.ToString());
            }

            return oficios;
        }
    }
}