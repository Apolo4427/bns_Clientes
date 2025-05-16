using ModuloClientes.Core.Models;
using ModuloClientes.Core.Ports.Queries.EmpresaQueries;
using ModuloClientes.Core.Ports.Repositories;

namespace ModuloClientes.Infrastructure.Persistence.Handlers.EmpresaHandler
{
    public class ListEmpresasHandler : IListEmpresasQueryHandler
    {

        private readonly IEmpresaRepository _repository;

        public ListEmpresasHandler(IEmpresaRepository repository)
            => _repository = repository;


        public async Task<IEnumerable<Empresa>> HandleAsync(ListEmpresasQuery query)
        {
            return await _repository.ListAsync();
        }
    }
}