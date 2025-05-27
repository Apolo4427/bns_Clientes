using MediatR;
using ModuloClientes.Core.Models.ValueObjects.ClienteValueObjects;
using ModuloClientes.Core.Ports.Commands.ClienteCommands;
using ModuloClientes.Core.Ports.Repositories;

namespace ModuloClientes.Infrastructure.Persistence.Handlers.ClienteHandler
{
    public class ReemplazarOficiosHandler : IRequestHandler<UpdateOficiosCommand, IEnumerable<string>>
    {
        private readonly IClienteRepository _repo;

        public ReemplazarOficiosHandler(IClienteRepository clienteRepository)
        {
            _repo = clienteRepository;
        }
        public async Task<IEnumerable<string>> Handle(UpdateOficiosCommand command, CancellationToken ct)
        {
            var cliente = await _repo.GetByIdAsync(command.ClienteId, ct)
                ?? throw new KeyNotFoundException($"El cliente con el id: {command.ClienteId} no se ha encontrado");

            var nuevosOficios = command.Oficios
                .Select(o => new Oficio(o))
                .ToList();

            cliente.ReemplazarOficios(nuevosOficios);
            await _repo.UpdateAsync(cliente, ct);

            return nuevosOficios.Select(o => o.ToString());
        }
    }
}