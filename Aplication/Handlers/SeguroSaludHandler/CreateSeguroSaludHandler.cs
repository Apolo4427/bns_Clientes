using MediatR;
using ModuloClientes.Aplication.Command.SeguroSaludCommands;
using ModuloClientes.Core.Ports.IRepositories;

namespace ModuloClientes.Aplication.Handlers.SeguroSaludHandler
{
   public class CreateSeguroSaludHandler : IRequestHandler<CreateSeguroSaludCommand, Guid>
   {
      private readonly ISeguroSaludRepository _repo;
      public CreateSeguroSaludHandler(ISeguroSaludRepository saludRepository)
      {
         _repo = saludRepository;
      }
      public Task<Guid> Handle(CreateSeguroSaludCommand command, CancellationToken cancellationToken)
      {
         
      }
   }
}