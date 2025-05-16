using ModuloClientes.Core.Models;
using ModuloClientes.Core.Ports.Queries.ClienteQueries;
using ModuloClientes.Core.Ports.Repositories;

namespace ModuloClientes.Infrastructure.Persistence.Handlers.ClienteHandler
{
    public class ListClientesHandler : IListClientesQueryHandler
    {

        private readonly IClienteRepository _repo;

        public ListClientesHandler(IClienteRepository repository)
        {
            _repo = repository;
        }

        public async Task<IEnumerable<Cliente>> HandleAsync(ListClientesQuery query)
        {
            
            return await _repo.ListAsync(query.PageNumber, query.PageSize);
        }
    }
}