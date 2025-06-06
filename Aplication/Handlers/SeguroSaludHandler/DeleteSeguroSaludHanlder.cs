using MediatR;
using ModuloClientes.Aplication.Command.SeguroSaludCommands;
using ModuloClientes.Core.Ports.IRepositories;

namespace ModuloClientes.Aplication.Handlers.SeguroSaludHandler
{
    public class DeleteSeguroSaludHandler : IRequestHandler<DeleteSeguroSaludCommand>
    {
        private readonly ISeguroSaludRepository _repo;
        public DeleteSeguroSaludHandler(ISeguroSaludRepository repo)
        {
            _repo = repo;
        }
        public async Task Handle(DeleteSeguroSaludCommand request, CancellationToken cancellationToken)
        {
        
            await _repo.DeleteAsync(request.Id, cancellationToken);

        }
    }
}