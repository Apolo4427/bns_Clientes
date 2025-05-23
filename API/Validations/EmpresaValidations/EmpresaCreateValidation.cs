using FluentValidation;
using ModuloClientes.API.DTOs.Create;
using ModuloClientes.Core.Models.ValueObjects.ClienteValueObjects;
using ModuloClientes.Core.Models.ValueObjects.EmpresaValueObjects;

namespace ModuloClientes.API.Validations.EmpresaValidations
{
    public class EmpresaCreateValidation : AbstractValidator<EmpresaCreateDto>
    {
        public EmpresaCreateValidation()
        {
            RuleFor(e => e.EIN)
                .NotEmpty()
                .WithMessage("El EIN es obligatorio")
                .Custom((ein, context) =>
                {
                    var result = EIN.TryCreate(ein);
                    if (!result.IsSuccess)
                        context.AddFailure(result.Error);
                });

            RuleFor(e => e.Nombre).NotEmpty().WithMessage("El nombre no puede estar vacio")
                .Custom((nombre, contexto)=>
                {
                    var result = CompanyName.TryCreate(nombre);
                    if (!result.IsSuccess)
                        contexto.AddFailure(result.Error);
                });

            RuleFor(e => e.Direccion).NotEmpty().WithMessage("La direccion no puede estar vacia")
                .Custom((direccion, contexto)=>
                    {
                        var result = Address.TryCreate(direccion);
                        if (!result.IsSuccess)
                            contexto.AddFailure(result.Error);
                    });

            RuleFor(e => e.CorreoContacto)
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

            RuleFor(e => e.Telefono)
                .NotEmpty()
                .WithMessage("El Telefono es obligatorio")
                .Custom((telefono, contexto) =>
                    {
                        var result = Phone.TryCreate(telefono);
                        if (!result.IsSuccess)
                            contexto.AddFailure(result.Error);
                    });

            RuleFor(c => c.FechaConstitucion)
                .Must(fecha => fecha != default(DateTime))
                .WithMessage("La fecha de constitucion no puede ser el valor por defecto")
                .LessThan(DateTime.Today)
                .WithMessage("La fecha de constitucion no puede ser futura");
        }
    }
}