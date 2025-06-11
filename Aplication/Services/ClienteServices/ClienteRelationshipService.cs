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
            var cliente = await _clienteRepo.GetByIdAsync(command.ClienteId, includeRelations: true);
            var otro = await _clienteRepo.GetByIdAsync(command.RelacionadoId, includeRelations: true);

            ClienteRelationshipServiceHelper.VincularBidireccional(
                cliente,
                otro,
                command.Tipo,
                command.EsDependiente);

            await _clienteRepo.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DesvincularRelacionAsync(
            DeleteClienteRelacionCommand command,
            CancellationToken cancellationToken = default)
        {
            var cliente = await _clienteRepo.GetByIdAsync(command.ClienteId, includeRelations: true);
            var otro = await _clienteRepo.GetByIdAsync(command.RelacionadoId, includeRelations: true);

            cliente.EliminarRelacion(command.RelacionadoId);
            otro.DesvincularRelacion(command.ClienteId);

            await _clienteRepo.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task CambiarTipoRelacionAsync(
            ChangeClienteRelacionTypeCommand command,
            CancellationToken cancellationToken = default)
        {
            var cliente = await _clienteRepo.GetByIdAsync(command.ClienteId, includeRelations: true);
            var otro = await _clienteRepo.GetByIdAsync(command.RelacionadoId, includeRelations: true);

            cliente.CambiarTipoRelacion(command.RelacionadoId, command.NuevoTipo);

            var inverso = command.NuevoTipo switch
            {
                TipoRelacion.Padre => TipoRelacion.Hijo,
                TipoRelacion.Hijo => TipoRelacion.Padre,
                _ => command.NuevoTipo
            };

            otro.CambiarTipoRelacion(command.ClienteId, inverso);

            await _clienteRepo.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task CambiarDependenciaRelacionAsync(
            ChangeClienteDependenciaCommand command,
            CancellationToken cancellationToken = default)
        {
            var cliente = await _clienteRepo.GetByIdAsync(command.ClienteId, includeRelations: true);
            var otro = await _clienteRepo.GetByIdAsync(command.RelacionadoId, includeRelations: true);

            cliente.CambiarDependenciaRelacion(command.RelacionadoId, command.EsDependiente);
            otro.CambiarDependenciaRelacion(command.ClienteId, !command.EsDependiente);

            await _clienteRepo.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}