using FluentValidation;
using ModuloClientes.API.DTOs.Update;

namespace ModuloClientes.API.Validations.ClienteValidations
{
    public class ClienteUpdateValidation : AbstractValidator<ClienteUpdateDto>
    {
        public ClienteUpdateValidation()
        {
            When(x => x.Nombre is not null, () =>
            {
                RuleFor(x => x.Nombre)
                    .NotEmpty().WithMessage("El nombre no puede estar vacío.")
                    .MaximumLength(100).WithMessage("El nombre no puede superar 100 caracteres.");
            });

            // Apellido
            When(x => x.Apellido is not null, () =>
            {
                RuleFor(x => x.Apellido)
                    .NotEmpty().WithMessage("El apellido no puede estar vacío.")
                    .MaximumLength(100).WithMessage("El apellido no puede superar 100 caracteres.");
            });

            // Correo
            When(x => x.Correo is not null, () =>
            {
                RuleFor(x => x.Correo)
                    .NotEmpty().WithMessage("El correo no puede estar vacío.")
                    .EmailAddress().WithMessage("Formato de correo inválido.")
                    .MaximumLength(150).WithMessage("El correo no puede superar 150 caracteres.");
            });

            // Teléfono
            When(x => x.Telefono is not null, () =>
            {
                RuleFor(x => x.Telefono)
                    .NotEmpty().WithMessage("El teléfono no puede estar vacío.")
                    .Matches(@"^\+\d{1,3}\(\d{3}\)\d{6,10}$")
                    .WithMessage("Formato inválido. Use +XX(XXX)XXXXXXX");
            });

            // Fecha de nacimiento (si no quieres forzarla, hazla nullable en el DTO)
            When(x => x.FechaDeNacimiento != default && x.FechaDeNacimiento is not null, () =>
            {
                RuleFor(x => x.FechaDeNacimiento)
                    .GreaterThan(new DateTime(1900, 1, 1))
                        .WithMessage("La fecha de nacimiento debe ser posterior al 1 de enero de 1900.")
                    .LessThanOrEqualTo(DateTime.Today)
                        .WithMessage("La fecha de nacimiento no puede ser futura.");
            });

            // Dirección
            When(x => x.Direccion is not null, () =>
            {
                RuleFor(x => x.Direccion)
                    .NotEmpty().WithMessage("La dirección no puede estar vacía.")
                    .MaximumLength(200).WithMessage("La dirección no puede superar 200 caracteres.");
            });

            // Estado civil
            When(x => x.EstadoCivil is not null, () =>
            {
                RuleFor(x => x.EstadoCivil)
                    .NotEmpty().WithMessage("El estado civil no puede estar vacío.")
                    .MaximumLength(50).WithMessage("El estado civil no puede superar 50 caracteres.");
            });

            // Estado tributario
            When(x => x.EstadoTributario is not null, () =>
            {
                RuleFor(x => x.EstadoTributario)
                    .NotEmpty().WithMessage("El estado tributario no puede estar vacío.")
                    .MaximumLength(50).WithMessage("El estado tributario no puede superar 50 caracteres.");
            });
        }
    }
}