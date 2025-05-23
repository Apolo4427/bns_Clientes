using FluentValidation;
using ModuloClientes.API.DTOs.Create;
using ModuloClientes.Core.Models.ValueObjects.ClienteValueObjects;

namespace ModuloClientes.API.Validations.ClienteValidations
{
    public class ClienteCreateValidation : AbstractValidator<ClienteCreateDto>
    {
        public ClienteCreateValidation()
        {
            RuleFor(c => c.Nombre).NotEmpty().WithMessage("El nombre es obligatorio")
            .Custom((nombre,contexto)=>
            {
                var result = Name.TryCreate(nombre);
                if (!result.IsSuccess)
                    contexto.AddFailure(result.Error);
            });

            RuleFor(c => c.Apellido).NotEmpty().WithMessage("El apellido es obligatorio")
                .Custom((apellido, contexto)=>
                {
                    var result = Surname.TryCreate(apellido);
                    if (!result.IsSuccess)
                        contexto.AddFailure(result.Error);
                });

            RuleFor(c => c.Correo)
                .NotEmpty()
                .WithMessage("El Correo es obligatorio")
                .EmailAddress()
                .WithMessage("El formato del correo electrónico no es válido")
                .Custom((correo, contexto)=>
                {
                    var result = Email.TryCreate(correo);
                    if (!result.IsSuccess)
                        contexto.AddFailure(result.Error);
                });

            RuleFor(c => c.Telefono)
                .NotEmpty()
                .WithMessage("El Telefono es obligatorio")
                .Custom((telefono, contexto)=>
                {
                    var result = Phone.TryCreate(telefono);
                    if (!result.IsSuccess)
                        contexto.AddFailure(result.Error);
                });

            RuleFor(c => c.EstadoCivil).NotEmpty().WithMessage("El EstadoCivil es obligatorio");

            RuleFor(c => c.EstadoTributario).NotEmpty().WithMessage("El EstadoTributario es obligatorio");
            
            RuleFor(c => c.SocialSecurityNumber)
                .NotEmpty()
                .WithMessage("El Social Security Number es obligatorio")
                .Custom((ssn, contexto)=>
                {
                    var result = SSN.TryCreate(ssn);
                    if (!result.IsSuccess)
                        contexto.AddFailure(result.Error);
                });

            RuleFor(c => c.Direccion).NotEmpty().WithMessage("El Direccion es obligatoria")
                .Custom((direccion, contexto)=>
                {
                    var result = Address.TryCreate(direccion);
                    if (!result.IsSuccess)
                        contexto.AddFailure(result.Error); 
                });

            RuleFor(c => c.FechaNacimiento)
                .NotEmpty()
                .WithMessage("La fecha de nacimiento es obligatoria")
                .Must(fecha => fecha != default(DateTime))
                .WithMessage("La fecha de nacimiento no puede ser el valor por defecto")
                .GreaterThanOrEqualTo(new DateTime(1900, 1, 1))
                .WithMessage("La fecha de nacimiento debe ser posterior a 1900")
                .LessThan(DateTime.Today)
                .WithMessage("La fecha de nacimiento no puede ser futura");                
        }
    }
}