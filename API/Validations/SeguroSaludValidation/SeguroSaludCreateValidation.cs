using FluentValidation;
using ModuloClientes.API.DTOs.Create;
using ModuloClientes.Core.Models.ValueObjects.EmpresaValueObjects;
using ModuloClientes.Core.Models.ValueObjects.SeguroSaludValueObjects;

namespace ModuloClientes.API.Validations.SeguroSaludValidation
{
    public class SeguroSaludCreateValidation : AbstractValidator<SeguroSaludCreateDto>
    {
        public SeguroSaludCreateValidation()
        {
            // Proveedor (CompanyName VO)
            RuleFor(x => x.Proveedor)
                .NotEmpty().WithMessage("El proveedor es obligatorio.")
                .Custom((proveedor, contexto) =>
                {
                    var resultado = CompanyName.TryCreate(proveedor);
                    if (!resultado.IsSuccess)
                        contexto.AddFailure(resultado.Error);
                });

            // NombrePlan (PlanName VO)
            RuleFor(x => x.NombrePlan)
                .NotEmpty().WithMessage("El nombre del plan es obligatorio.")
                .Custom((nombrePlan, contexto) =>
                {
                    var resultado = PlanName.TryCreate(nombrePlan);
                    if (!resultado.IsSuccess)
                        contexto.AddFailure(resultado.Error);
                });

            // NumeroPoliza (PolicyNumber VO)
            RuleFor(x => x.NumeroPoliza)
                .NotEmpty().WithMessage("El número de póliza es obligatorio.")
                .Custom((numeroPoliza, contexto) =>
                {
                    var resultado = PolicyNumber.TryCreate(numeroPoliza);
                    if (!resultado.IsSuccess)
                        contexto.AddFailure(resultado.Error);
                });

            // FechaInicio
            RuleFor(x => x.FechaInicio)
                .NotEmpty().WithMessage("La fecha de inicio es obligatoria.")
                .Must(fecha => fecha != default)
                    .WithMessage("La fecha de inicio no puede ser el valor por defecto.")
                .LessThanOrEqualTo(DateTime.Today)
                    .WithMessage("La fecha de inicio no puede ser futura.");

            // FechaFin
            RuleFor(x => x.FechaFin)
                .NotEmpty().WithMessage("La fecha de fin es obligatoria.")
                .Must((dto, fechaFin) => fechaFin > dto.FechaInicio)
                    .WithMessage("La fecha de fin debe ser posterior a la fecha de inicio.");

            // PrimaMensual
            RuleFor(x => x.PrimaMensual)
                .GreaterThanOrEqualTo(0m)
                .WithMessage("La prima mensual debe ser un valor no negativo.");
        }
    }
}