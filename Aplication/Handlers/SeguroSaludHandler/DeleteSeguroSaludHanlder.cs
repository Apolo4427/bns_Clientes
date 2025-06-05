using MediatR;
using Microsoft.EntityFrameworkCore;
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
            try
            {
                await _repo.DeleteAsync(request.Id, cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new DbUpdateConcurrencyException(
                    $"El seguro de id {request.Id} ya ha sido eliminado por otro proceso"
                );
            }

        }
    }
}