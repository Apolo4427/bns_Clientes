using FluentValidation;
using ModuloClientes.Aplication.Command.SeguroSaludCommands;
using ModuloClientes.Core.Models.ValueObjects.EmpresaValueObjects;
using ModuloClientes.Core.Models.ValueObjects.SeguroSaludValueObjects;

namespace ModuloClientes.API.Validations.SeguroSaludValidations
{
    public class SeguroSaludUpdateValidation : AbstractValidator<UpdateSeguroSaludCommand>
    {
        public SeguroSaludUpdateValidation()
        {
            // Id: no debe ser Guid vacío
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("El Id del seguro de salud es obligatorio.");

            // RowVersion (concurrencia): debe existir
            RuleFor(x => x.RowVersion)
                .NotNull().WithMessage("El token de concurrencia es obligatorio.")
                .Must(rv => rv.Length > 0)
                    .WithMessage("El token de concurrencia no puede estar vacío.");

            // Proveedor: si viene no nulo, validar VO
            When(x => x.Proveedor is not null, () =>
            {
                RuleFor(x => x.Proveedor!)
                    .NotEmpty().WithMessage("El proveedor no puede estar vacío.")
                    .Custom((proveedor, ctx) =>
                    {
                        var resultado = CompanyName.TryCreate(proveedor);
                        if (!resultado.IsSuccess)
                            ctx.AddFailure(resultado.Error);
                    });
            });

            // NombrePlan: si viene no nulo, validar VO
            When(x => x.NombrePlan is not null, () =>
            {
                RuleFor(x => x.NombrePlan!)
                    .NotEmpty().WithMessage("El nombre del plan no puede estar vacío.")
                    .Custom((nombrePlan, ctx) =>
                    {
                        var resultado = PlanName.TryCreate(nombrePlan);
                        if (!resultado.IsSuccess)
                            ctx.AddFailure(resultado.Error);
                    });
            });

            // NumeroPoliza: si viene no nulo, validar VO
            When(x => x.NumeroPoliza is not null, () =>
            {
                RuleFor(x => x.NumeroPoliza!)
                    .NotEmpty().WithMessage("El número de póliza no puede estar vacío.")
                    .Custom((numeroPoliza, ctx) =>
                    {
                        var resultado = PolicyNumber.TryCreate(numeroPoliza);
                        if (!resultado.IsSuccess)
                            ctx.AddFailure(resultado.Error);
                    });
            });

            // FechaInicio: si viene, no puede ser futura ni default
            When(x => x.FechaInicio.HasValue, () =>
            {
                RuleFor(x => x.FechaInicio.Value)
                    .Must(fecha => fecha != default)
                        .WithMessage("La fecha de inicio no puede ser el valor por defecto.")
                    .LessThanOrEqualTo(DateTime.Today)
                        .WithMessage("La fecha de inicio no puede ser posterior a hoy.");
            });

            // FechaFin: si viene, debe ser posterior a FechaInicio si ésta también viene
            When(x => x.FechaFin.HasValue, () =>
            {
                RuleFor(x => x)
                    .Must(x =>
                    {
                        // Si sólo FechaFin se proporciona, validar que no sea default
                        if (!x.FechaInicio.HasValue)
                            return x.FechaFin.Value != default;

                        // Si ambas fechas vienen, FechaFin > FechaInicio
                        return x.FechaFin.Value > x.FechaInicio.Value;
                    })
                    .WithMessage("La fecha de fin debe ser posterior a la fecha de inicio, y no puede ser el valor por defecto.");
            });

            // PrimaMensual: si viene, no puede ser negativa
            When(x => x.PrimaMensual.HasValue, () =>
            {
                RuleFor(x => x.PrimaMensual.Value)
                    .GreaterThanOrEqualTo(0m)
                        .WithMessage("La prima mensual debe ser un valor no negativo.");
            });
        }
    }
}
