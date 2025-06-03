using MediatR;
using ModuloClientes.Aplication.Command.ClienteCommands;
using ModuloClientes.Core.Models.ValueObjects.ClienteValueObjects;
using ModuloClientes.Core.Ports.IRepositories;

namespace ModuloClientes.Aplication.Handlers.ClienteHandler
{
    public class AgregarOficioHandler : IRequestHandler<AgregarOficioCommand, IEnumerable<string>>
    {
        private readonly IClienteRepository _repo;

        public AgregarOficioHandler(IClienteRepository clienteRepository)
        {
            _repo = clienteRepository;
        }

        public async Task<IEnumerable<string>> Handle(AgregarOficioCommand command, CancellationToken ct)
        {
            var cliente = await _repo.GetByIdAsync(command.ClienteId, ct)
                ?? throw new KeyNotFoundException($"No se ha encontrado un cliente con el a Id: {command.ClienteId}");

            var nuevoOficio = new Oficio(command.Oficio);

            cliente.AgregarOficio(nuevoOficio);

            await _repo.UpdateAsync(cliente, ct);

            var response = new List<string>();

            foreach (var o in cliente.Oficios)
            {
                response.Add(o.ToString());
            }

            return response;
        }
    }
}