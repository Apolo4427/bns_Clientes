using FluentValidation;
using ModuloClientes.API.DTOs.Update;
using ModuloClientes.Core.Models.ValueObjects.ClienteValueObjects;
using ModuloClientes.Core.Models.ValueObjects.EmpresaValueObjects;

namespace ModuloClientes.API.Validations.EmpresaValidations
{
    public class EmpresaUpdateValidation : AbstractValidator<EmpresaUpdateDto>
    {
        public EmpresaUpdateValidation()
        {
            RuleFor(dto => dto.RowVersion)
                .NotEmpty().WithMessage("La version de la fila es obligatoria")
                .Must(rv =>
                {
                    try { Convert.FromBase64String(rv); return true; }
                    catch { return false; }
                }).WithMessage("RowVersion invalido");

            When(dto => dto.Nombre is not null, () =>
            {
                RuleFor(dto => dto.Nombre).NotEmpty().WithMessage("El nombre no puede estar vacio")
                    .Custom((nombre, contexto) =>
                    {
                        var resul = CompanyName.TryCreate(nombre);
                        if (!resul.IsSuccess)
                            contexto.AddFailure(resul.Error);
                    });
            });

            When(dto => dto.EIN is not null, () =>
            {
                RuleFor(dto => dto.EIN).NotEmpty().WithMessage("El EIN no puede estar vacio")
                    .Custom((ein, contexto) =>
                    {
                        var resul = EIN.TryCreate(ein);
                        if (!resul.IsSuccess)
                            contexto.AddFailure(resul.Error);
                    });
            });

            // Dirección
            When(dto => dto.Direccion is not null, () =>
            {
                RuleFor(dto => dto.Direccion)
                    .NotEmpty().WithMessage("La direccion no puede estar vacio")
                    .Custom((direccion, contexto) =>
                    {
                        var resul = Address.TryCreate(direccion);
                        if (!resul.IsSuccess)
                            contexto.AddFailure(resul.Error);
                    });
            });

            // Teléfono
            When(dto => dto.Telefono is not null, () =>
            {
                RuleFor(dto => dto.Telefono)
                    .NotEmpty().WithMessage("El telefono no puede estar vacio")
                    .Custom((telefono, contexto) =>
                    {
                        var resul = Phone.TryCreate(telefono);
                        if (!resul.IsSuccess)
                            contexto.AddFailure(resul.Error);
                    });
            });

            // correo de contacto
            When(dto => dto.CorreoContacto is not null, () =>
            {
                RuleFor(dto => dto.CorreoContacto).NotEmpty().WithMessage("El correo de contacto no puede estar vacio")
                    .Custom((correo, contexto) =>
                    {
                        var result = Email.TryCreate(correo);
                        if (!result.IsSuccess)
                            contexto.AddFailure(result.Error);
                    });
            });

            // Fecha de constitución
            When(dto => dto.FechaConstitucion.HasValue, () =>
            {
                RuleFor(dto => dto.FechaConstitucion.Value)
                    .GreaterThan(new DateTime(1900, 1, 1))
                        .WithMessage("La fecha de constitucion debe ser posterior al 1 de enero de 1900")
                    .LessThan(DateTime.Today)
                        .WithMessage("La fecha de constitucion no puede ser futura");
            });

        }
    }
}