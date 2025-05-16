using FluentValidation;
using ModuloClientes.API.DTOs.Create;

namespace ModuloClientes.API.Validations.ClienteValidations
{
    public class ClienteCreateValidation : AbstractValidator<ClienteCreateDto>
    {
        public ClienteCreateValidation()
        {
            RuleFor(c => c.Nombre).NotEmpty().WithMessage("El nombre es obligatorio");

            RuleFor(c => c.Apellido).NotEmpty().WithMessage("El apellido es obligatorio");

            RuleFor(c => c.Correo)
                .NotEmpty()
                .WithMessage("El Correo es obligatorio")
                .EmailAddress()
                .WithMessage("El formato del correo electrónico no es válido");

            RuleFor(c => c.Telefono)
                .NotEmpty()
                .WithMessage("El Telefono es obligatorio")
                .Matches(@"^\+\d{1,3}\(\d{3}\)\d{6,10}$")
                .WithMessage("Formato inválido. Use +XX(XXX)XXXXXXX");

            RuleFor(c => c.EstadoCivil).NotEmpty().WithMessage("El EstadoCivil es obligatorio");

            RuleFor(c => c.EstadoTributario).NotEmpty().WithMessage("El EstadoTributario es obligatorio");
            
            RuleFor(c => c.SocialSecurityNumber)
                .NotEmpty()
                .WithMessage("El Social Security Number es obligatorio")
                .Matches(@"^\d{3}-\d{2}-\d{4}$")
                .WithMessage("Formato inválido. Use XXX-XX-XXXX");;

            RuleFor(c => c.Direccion).NotEmpty().WithMessage("El Direccion es obligatoria");

            RuleFor(c => c.FechaNacimiento)
                .NotEmpty()
                .WithMessage("La fecha de nacimiento es obligatoria")
                .Must(fecha => fecha != default(DateTime))
                .WithMessage("La fecha de nacimiento no puede ser el valor por defecto")
                .LessThan(DateTime.Today)
                .WithMessage("La fecha de nacimiento no puede ser futura")
                .GreaterThanOrEqualTo(new DateTime(1900, 1, 1))
                .WithMessage("La fecha de nacimiento debe ser posterior a 1900");
        }
    }
}