using MediatR;
using ModuloClientes.Core.Models.ValueObjects.ClienteValueObjects;
using ModuloClientes.Core.Ports.Commands.ClienteCommands;
using ModuloClientes.Core.Ports.Repositories;

namespace ModuloClientes.Infrastructure.Persistence.Handlers.ClienteHandler
{
    public class EliminarOficioHandler : IRequestHandler<EliminarOficioCommand, IEnumerable<string>>
    {
        private readonly IClienteRepository _repo;

        public EliminarOficioHandler(IClienteRepository clienteRepository)
        {
            _repo = clienteRepository;
        }
        public async Task<IEnumerable<string>> Handle(EliminarOficioCommand command, CancellationToken ct)
        {
            var cliente = await _repo.GetByIdAsync(command.ClienteId, ct)
                ?? throw new KeyNotFoundException($"El cluente con el id: {command.ClienteId} no se ha encontrado");

            var oficioEliminado = new Oficio(command.Oficio);

            cliente.EliminarOficio(oficioEliminado);

            await _repo.UpdateAsync(cliente, ct);

            var oficios = new List<string>();

            foreach (var o in cliente.Oficios)
            {
                oficios.Add(o.ToString());
            }

            return oficios;
        }
    }
}