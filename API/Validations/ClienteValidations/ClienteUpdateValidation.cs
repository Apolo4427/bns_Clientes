using FluentValidation;
using ModuloClientes.API.DTOs.Update;

namespace ModuloClientes.API.Validations.ClienteValidations
{
    public class ClienteUpdateValidation : AbstractValidator<ClienteUpdateDto>
    {
        public ClienteUpdateValidation()
        {
            
        }
    }
}