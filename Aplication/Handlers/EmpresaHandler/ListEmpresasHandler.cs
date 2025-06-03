using MediatR;
using ModuloClientes.Core.Models;
using ModuloClientes.Core.Ports.Queries.EmpresaQueries;
using ModuloClientes.Core.Ports.IRepositories;

namespace ModuloClientes.Aplication.Handlers.EmpresaHandler
{
    public class ListEmpresasHandler : IRequestHandler<ListEmpresasQuery, IReadOnlyList<Empresa>>
    {

        private readonly IEmpresaRepository _repository;

        public ListEmpresasHandler(IEmpresaRepository repository)
            => _repository = repository;


        public async Task<IReadOnlyList<Empresa>> Handle(ListEmpresasQuery query, CancellationToken ct)
        {
            return await _repository.ListAsync(query.PageNumber, query.PageSize, ct);
        }
    }
}