using MediatR;
using ModuloClientes.Core.Models;
using ModuloClientes.Core.Ports.Queries.EmpresaQueries;
using ModuloClientes.Core.Ports.Repositories;

namespace ModuloClientes.Infrastructure.Persistence.Handlers.EmpresaHandler
{
    public class GetEmpresaByIdHandler : IRequestHandler<GetEmpresaByIdQuery, Empresa>
    {
        private readonly IEmpresaRepository _repository;

        public GetEmpresaByIdHandler(IEmpresaRepository repository)
            => _repository = repository;

        public async Task<Empresa> Handle(GetEmpresaByIdQuery query, CancellationToken ct)
        {
            return await _repository.GetByIdAsync(query.Id, ct);
        }
    }
}