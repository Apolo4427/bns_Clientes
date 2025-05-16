using FluentValidation;
using ModuloClientes.API.DTOs.Create;

namespace ModuloClientes.API.Validations.EmpresaValidations
{
    public class EmpresaCreateValidation : AbstractValidator<EmpresaCreateDto>
    {
        public EmpresaCreateValidation()
        {
            RuleFor(e => e.EIN)
                .NotEmpty()
                .WithMessage("El EIN es obligatorio")
                .Matches(@"^(0[1-6]|1[0-6]|[2-7][0-9]|8[0-9]|9[0-9])-[0-9]{7}$")
                .WithMessage("Formato inválido. Use XX-XXXXXXX con prefijo válido");

            RuleFor(e => e.Nombre).NotEmpty().WithMessage("El nombre no puede estar vacio");

            RuleFor(e => e.Direccion).NotEmpty().WithMessage("La direccion no puede estar vacia");

            RuleFor(e => e.CorreoContacto)
                .NotEmpty()
                .WithMessage("El Correo es obligatorio")
                .EmailAddress()
                .WithMessage("El formato del correo electrónico no es válido");

            RuleFor(e => e.Telefono)
                .NotEmpty()
                .WithMessage("El Telefono es obligatorio")
                .Matches(@"^\+\d{1,3}\(\d{3}\)\d{6,10}$")
                .WithMessage("Formato inválido. Use +XX(XXX)XXXXXXX");

            RuleFor(c => c.FechaConstitucion)
                .Must(fecha => fecha != default(DateTime))
                .WithMessage("La fecha de constitucion no puede ser el valor por defecto")
                .LessThan(DateTime.Today)
                .WithMessage("La fecha de constitucion no puede ser futura");
        }
    }
}