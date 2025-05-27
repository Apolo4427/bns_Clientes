using MediatR;
using ModuloClientes.Core.Models;
using ModuloClientes.Core.Ports.Queries.ClienteQueries;
using ModuloClientes.Core.Ports.Repositories;

namespace ModuloClientes.Infrastructure.Persistence.Handlers.ClienteHandler
{
    public class GetClienteByIdHandler : IRequestHandler<GetClienteByIdQuery, Cliente>
    {

        public readonly IClienteRepository _repo;

        public GetClienteByIdHandler(IClienteRepository repository)
        {
            _repo = repository;
        }

        public async Task<Cliente> Handle(GetClienteByIdQuery query, CancellationToken ct)
        {
            return await _repo.GetByIdAsync(query.Id, ct);
        }
    }
}