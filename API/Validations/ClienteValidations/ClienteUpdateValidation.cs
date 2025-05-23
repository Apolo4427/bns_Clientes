using FluentValidation;
using ModuloClientes.API.DTOs.Update;
using ModuloClientes.Core.Models.ValueObjects.ClienteValueObjects;

namespace ModuloClientes.API.Validations.ClienteValidations
{
    public class ClienteUpdateValidation : AbstractValidator<ClienteUpdateDto>
    {
        public ClienteUpdateValidation()
        {

            // RowVersion (Base64)
            RuleFor(x => x.RowVersion)
                .NotEmpty().WithMessage("La versión de fila es obligatoria")
                .Must(rv =>
                {
                    try { Convert.FromBase64String(rv); return true; }
                    catch { return false; }
                })
                .WithMessage("RowVersion inválida");

            // Nombre
            When(x => x.Nombre is not null, () =>
            {
                RuleFor(x => x.Nombre)
                    .NotEmpty().WithMessage("El nombre no puede estar vacío.")
                    .Custom((nommbre, context) =>
                    {
                        var result = Name.TryCreate(nommbre);
                        if (!result.IsSuccess)
                            context.AddFailure(result.Error);
                    });
            });

            // Apellido
            When(x => x.Apellido is not null, () =>
            {
                RuleFor(x => x.Apellido)
                    .NotEmpty().WithMessage("El apellido no puede estar vacío.")
                    .Custom((apellido, contexto) =>
                    {
                        var result = Surname.TryCreate(apellido);
                        if (!result.IsSuccess)
                            contexto.AddFailure(result.Error);
                    });
            });

            // Correo
            When(x => x.Correo is not null, () =>
            {
                RuleFor(x => x.Correo)
                    .NotEmpty().WithMessage("El correo no puede estar vacío.")
                    .EmailAddress().WithMessage("Formato de correo inválido.")
                    .Custom((correo, contexto) =>
                    {
                        var result = Email.TryCreate(correo);
                        if (!result.IsSuccess)
                            contexto.AddFailure(result.Error);
                    });
            });

            // Teléfono
            When(x => x.Telefono is not null, () =>
            {
                RuleFor(x => x.Telefono)
                    .NotEmpty().WithMessage("El teléfono no puede estar vacío.")
                    .Custom((telefono, contexto) =>
                    {
                        var result = Phone.TryCreate(telefono);
                        if (!result.IsSuccess)
                            contexto.AddFailure(result.Error);
                    });
            });

            // Fecha de nacimiento (si no quieres forzarla, hazla nullable en el DTO)
            When(x => x.FechaDeNacimiento.HasValue, () =>
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
                    .Custom((direccion, contexto) =>
                    {
                        var result = Address.TryCreate(direccion);
                        if (!result.IsSuccess)
                            contexto.AddFailure(result.Error);
                    });
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

            When(dot => dot.SocialSecurityNumber is not null, () =>
            {
                RuleFor(dto => dto.SocialSecurityNumber)
                    .NotEmpty().WithMessage("El Social Security Number no puede estar vacio")
                    .Custom((ssn,contexto)=>
                    {
                        var result = SSN.TryCreate(ssn);
                        if (!result.IsSuccess)
                            contexto.AddFailure(result.Error);
                    });
            });
        }
    }
}