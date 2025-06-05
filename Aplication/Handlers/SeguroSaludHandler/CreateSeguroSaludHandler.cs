using AutoMapper;
using MediatR;
using ModuloClientes.Aplication.Command.SeguroSaludCommands;
using ModuloClientes.Core.Models;
using ModuloClientes.Core.Ports.IRepositories;

namespace ModuloClientes.Aplication.Handlers.SeguroSaludHandler
{
   public class CreateSeguroSaludHandler : IRequestHandler<CreateSeguroSaludCommand, Guid>
   {
      private readonly ISeguroSaludRepository _repo;
      private readonly IMapper _mapper;
      public CreateSeguroSaludHandler(ISeguroSaludRepository saludRepository, IMapper mapper)
      {
         _repo = saludRepository;
         _mapper = mapper;
      }
      public async Task<Guid> Handle(CreateSeguroSaludCommand command, CancellationToken cancellationToken)
      {
         var seguroSalud = _mapper.Map<SeguroSalud>(command);
         await _repo.AddAsync(seguroSalud, cancellationToken);
         return seguroSalud.Id;
      }
   }
}