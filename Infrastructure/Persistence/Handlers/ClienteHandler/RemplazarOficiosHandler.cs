using ModuloClientes.Core.Models.ValueObjects.ClienteValueObjects;
using ModuloClientes.Core.Ports.Commands.ClienteCommands;
using ModuloClientes.Core.Ports.Repositories;

namespace ModuloClientes.Infrastructure.Persistence.Handlers.ClienteHandler
{
    public class ReemplazarOficiosHandler : IUpdateOficiosCommandHandler
    {
        private readonly IClienteRepository _repo;

        public ReemplazarOficiosHandler(IClienteRepository clienteRepository)
        {
            _repo = clienteRepository;
        }
        public async Task<IEnumerable<string>> HandleAsync(UpdateOficiosCommand command)
        {
            var cliente = await _repo.GetByIdAsync(command.ClienteId);

            var nuevosOficios = command.Oficios
                .Select(o => new Oficio(o))
                .ToList();

            cliente.ReemplazarOficios(nuevosOficios);
            await _repo.UpdateAsync(cliente);

            return nuevosOficios.Select(o => o.ToString());
        }
    }
}