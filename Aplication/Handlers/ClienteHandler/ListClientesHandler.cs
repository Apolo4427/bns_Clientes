using MediatR;
using ModuloClientes.Core.Models;
using ModuloClientes.Core.Ports.Queries.ClienteQueries;
using ModuloClientes.Core.Ports.IRepositories;

namespace ModuloClientes.Aplication.Handlers.ClienteHandler
{
    public class ListClientesHandler : IRequestHandler<ListClientesQuery, IReadOnlyList<Cliente>>
    {

        private readonly IClienteRepository _repo;

        public ListClientesHandler(IClienteRepository repository)
        {
            _repo = repository;
        }

        public async Task<IReadOnlyList<Cliente>> Handle(ListClientesQuery query, CancellationToken ct)
        {
            
            return await _repo.ListAsync(query.PageNumber, query.PageSize, ct);
        }
    }
}