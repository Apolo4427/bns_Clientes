using ModuloClientes.Core.Models;
using ModuloClientes.Core.Ports.Queries.ClienteQueries;
using ModuloClientes.Core.Ports.Repositories;

namespace ModuloClientes.Infrastructure.Persistence.Handlers.ClienteHandler
{
    public class GetClienteByIdHandler : IGetClienteByIdQueryHandler
    {

        public readonly IClienteRepository _repo;

        public GetClienteByIdHandler(IClienteRepository repository)
        {
            _repo = repository;
        }

        public async Task<Cliente> HandleAsync(GetClienteByIdQuery query)
        {
            return await _repo.GetByIdAsync(query.Id);
        }
    }
}