using ModuloClientes.Core.Models.ValueObjects.EmpresaValueObjects;
using ModuloClientes.Core.Models.ValueObjects.SeguroSaludValueObjects;

namespace ModuloClientes.Core.Models
{
    public class SeguroSalud
    {
        public Guid Id { get; private set; }

        // Value Objects
        public CompanyName Proveedor { get; private set; }
        public PlanName NombrePlan { get; private set; }
        public PolicyNumber NumeroPoliza { get; private set; }

        // Fechas de vigencia
        public DateTime FechaInicio { get; private set; }
        public DateTime FechaFin { get; private set; }

        // Monto de la prima mensual
        public decimal PrimaMensual { get; private set; }

        // Clientes afiliados
        public ICollection<Cliente> Clientes { get; private set; } = new List<Cliente>();

        // Constructor para EF Core
        private SeguroSalud() { }

        // Constructor de dominio
        public SeguroSalud(
            CompanyName proveedor,
            PlanName nombrePlan,
            PolicyNumber numeroPoliza,
            DateTime fechaInicio,
            DateTime fechaFin,
            decimal primaMensual)
        {
            Id = Guid.NewGuid();
            Proveedor = proveedor ?? throw new ArgumentNullException(nameof(proveedor));
            NombrePlan = nombrePlan ?? throw new ArgumentNullException(nameof(nombrePlan));
            NumeroPoliza = numeroPoliza ?? throw new ArgumentNullException(nameof(numeroPoliza));

            if (fechaInicio == default)
                throw new ArgumentException("Fecha de inicio inválida.", nameof(fechaInicio));
            if (fechaFin <= fechaInicio)
                throw new ArgumentException("La fecha de fin debe ser posterior a la de inicio.", nameof(fechaFin));

            FechaInicio = fechaInicio;
            FechaFin = fechaFin;

            if (primaMensual < 0)
                throw new ArgumentOutOfRangeException(nameof(primaMensual), "La prima mensual debe ser no negativa.");

            PrimaMensual = primaMensual;
        }

        // Métodos de actualización
        public void CambiarProveedor(CompanyName nuevoProveedor)
        {
            if (nuevoProveedor is null)
                throw new ArgumentNullException(nameof(nuevoProveedor));
            if (nuevoProveedor.Equals(Proveedor))
                return;
            Proveedor = nuevoProveedor;
        }

        public void CambiarNombrePlan(PlanName nuevoNombrePlan)
        {
            if (nuevoNombrePlan is null)
                throw new ArgumentNullException(nameof(nuevoNombrePlan));
            if (nuevoNombrePlan.Equals(NombrePlan))
                return;
            NombrePlan = nuevoNombrePlan;
        }

        public void CambiarNumeroPoliza(PolicyNumber nuevoNumero)
        {
            if (nuevoNumero is null)
                throw new ArgumentNullException(nameof(nuevoNumero));
            if (nuevoNumero.Equals(NumeroPoliza))
                return;
            NumeroPoliza = nuevoNumero;
        }

        public void CambiarFechaInicio(DateTime nuevaFechaInicio)
        {
            if (nuevaFechaInicio == default)
                throw new ArgumentException("Fecha de inicio inválida.", nameof(nuevaFechaInicio));
            if (nuevaFechaInicio > DateTime.Today)
                throw new ArgumentException("La fecha de inicio no puede ser futura.", nameof(nuevaFechaInicio));
            if (nuevaFechaInicio.Equals(FechaInicio))
                return;
            FechaInicio = nuevaFechaInicio;
        }

        public void RenovarPoliza(DateTime nuevaFechaFin)
        {
            if (nuevaFechaFin <= FechaFin)
                throw new ArgumentException("La nueva fecha de fin debe ser posterior a la fecha actual de fin.", nameof(nuevaFechaFin));
            FechaFin = nuevaFechaFin;
        }

        public void CambiarPrimaMensual(decimal nuevaPrima)
        {
            if (nuevaPrima < 0)
                throw new ArgumentOutOfRangeException(nameof(nuevaPrima), "La prima mensual no puede ser negativa.");
            if (nuevaPrima == PrimaMensual)
                return;
            PrimaMensual = nuevaPrima;
        }

        // Asociación con clientes
        public void AfiliarCliente(Cliente cliente)
        {
            if (cliente is null)
                throw new ArgumentNullException(nameof(cliente));
            if (Clientes.Contains(cliente))
                return;
            Clientes.Add(cliente);
        }

        public void DesafiliarCliente(Cliente cliente)
        {
            if (cliente is null)
                throw new ArgumentNullException(nameof(cliente));
            if (!Clientes.Contains(cliente))
                throw new InvalidOperationException("El cliente no está afiliado a esta póliza.");
            Clientes.Remove(cliente);
        }
    }
}