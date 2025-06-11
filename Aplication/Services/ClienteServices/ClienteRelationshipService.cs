using ModuloClientes.Aplication.Command.ClienteCommands;
using ModuloClientes.Core.Enums;
using ModuloClientes.Core.Models;
using ModuloClientes.Core.Ports.IRepositories;
using ModuloClientes.Core.Ports.IServices.IClienteServices;

namespace ModuloClientes.Aplication.Services.ClienteDomainServices
{
    public class ClienteRelationshipService : IClienteRelationshipService
    {
        private readonly IClienteRepository _clienteRepo;

        public ClienteRelationshipService(IClienteRepository clienteRepo)
        {
            _clienteRepo = clienteRepo;
        }

        public async Task VincularRelacionAsync(
            AddClienteRelacionCommand command,
            CancellationToken cancellationToken = default)
        {
            var cliente = await _clienteRepo.GetByIdAsync(command.ClienteId, includeRelations: true, cancellationToken);
            var otro = await _clienteRepo.GetByIdAsync(command.RelacionadoId, includeRelations: true, cancellationToken);

            ClienteRelationshipServiceHelper.VincularBidireccional(
                cliente,
                otro,
                command.Tipo,
                command.EsDependiente);

            await _clienteRepo.SaveChangesAsync(cancellationToken);
        }

        public async Task DesvincularRelacionAsync(
            DeleteClienteRelacionCommand command,
            CancellationToken cancellationToken = default)
        {
            var cliente = await _clienteRepo.GetByIdAsync(command.ClienteId, includeRelations: true, cancellationToken);
            var otro = await _clienteRepo.GetByIdAsync(command.RelacionadoId, includeRelations: true, cancellationToken);

            cliente.EliminarRelacion(command.RelacionadoId);
            otro.EliminarRelacion(command.ClienteId);

            await _clienteRepo.SaveChangesAsync(cancellationToken);
        }

        public async Task CambiarTipoRelacionAsync(
            ChangeClienteRelacionTypeCommand command,
            CancellationToken cancellationToken = default)
        {
            var cliente = await _clienteRepo.GetByIdAsync(command.ClienteId, includeRelations: true, cancellationToken);
            var otro = await _clienteRepo.GetByIdAsync(command.RelacionadoId, includeRelations: true, cancellationToken);

            cliente.CambiarTipoRelacion(command.RelacionadoId, command.NuevoTipo);

            var inverso = command.NuevoTipo switch
            {
                TipoRelacion.Padre => TipoRelacion.Hijo,
                TipoRelacion.Hijo => TipoRelacion.Padre,
                _ => command.NuevoTipo
            };

            otro.CambiarTipoRelacion(command.ClienteId, inverso);

            await _clienteRepo.SaveChangesAsync(cancellationToken);
        }

        public async Task CambiarDependenciaRelacionAsync(
            ChangeClienteDependenciaCommand command,
            CancellationToken cancellationToken = default)
        {
            var cliente = await _clienteRepo.GetByIdAsync(command.ClienteId, includeRelations: true, cancellationToken);
            var otro = await _clienteRepo.GetByIdAsync(command.RelacionadoId, includeRelations: true, cancellationToken);

            cliente.CambiarDependenciaRelacion(command.RelacionadoId, command.EsDependiente);
            otro.CambiarDependenciaRelacion(command.ClienteId, !command.EsDependiente);

            await _clienteRepo.SaveChangesAsync(cancellationToken);
        }
    }
}