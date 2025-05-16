namespace ModuloClientes.Core.Models
{
    public class SeguroSalud
    {
        public int Id { get; private set; }
        public string Proveedor { get; private set; }
        public string NombrePlan { get; private set; }
        public string NumeroPoliza { get; private set; }
        public DateTime FechaInicio { get; private set; }
        public DateTime FechaFin { get; private set; }
        public decimal PrimaMensual { get; private set; }

        public ICollection<Cliente> Clientes { get; private set; } = new List<Cliente>();

        // Constructor vacio
        private SeguroSalud()
        {
#pragma warning disable CS8618
#pragma warning restore CS8618
        }

        public SeguroSalud(
            string proveedor,
            string nombrePlan,
            string numeroPoliza,
            DateTime fechaInicio,
            DateTime fechaFin,
            decimal primaMensual)
        {
            if (string.IsNullOrWhiteSpace(proveedor))
                throw new ArgumentException("El proveedor es obligatorio", nameof(proveedor));
            if (string.IsNullOrWhiteSpace(nombrePlan))
                throw new ArgumentException("El nombre del plan es obligatorio", nameof(nombrePlan));
            if (string.IsNullOrWhiteSpace(numeroPoliza))
                throw new ArgumentException("El número de póliza es obligatorio", nameof(numeroPoliza));
            if (fechaFin <= fechaInicio)
                throw new ArgumentException("La fecha de fin debe ser posterior a la de inicio", nameof(fechaFin));
            if (primaMensual < 0)
                throw new ArgumentOutOfRangeException(nameof(primaMensual), "La prima mensual debe ser no negativa");

            Proveedor = proveedor;
            NombrePlan = nombrePlan;
            NumeroPoliza = numeroPoliza;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            PrimaMensual = primaMensual;
        }

        public void RenovarPolizaActual(DateTime nuevaFechaFin)
        {
            if (nuevaFechaFin <= FechaFin)
                throw new ArgumentException("La nueva fecha de fin debe ser posterior a la fecha actual de fin", nameof(nuevaFechaFin));
            FechaFin = nuevaFechaFin;
        }

        public void CambiarProveedor(string proveedor)
        {
            if (string.IsNullOrWhiteSpace(proveedor))
                throw new ArgumentException("El proveedor no puede estar vacio", nameof(proveedor));
            Proveedor = proveedor;
        }

        public void CambiarNombrePlan(string nombrePlan)
        {
            if (string.IsNullOrWhiteSpace(nombrePlan))
                throw new ArgumentException("E' nombre del plan no puede estar vacio", nameof(nombrePlan));
            NombrePlan = nombrePlan;
        }

        public void CambiarNumeroPoliza(string numeroPoliza)
        {
            if (string.IsNullOrWhiteSpace(numeroPoliza))
                throw new ArgumentException("El numero de la poliza no puede estar vacio", nameof(numeroPoliza));
            NumeroPoliza = numeroPoliza;
        }

        public void CambiarFechaInicio(DateTime fecha)
        {
            if (fecha > DateTime.Now)
                throw new ArgumentException("La fecha no puede ser superior a la fecha actual", nameof(fecha));
            FechaInicio = fecha;
        }

        public void CambiarPrimaMensual(decimal prima)
        {
            if (prima < 0)
                throw new ArgumentException("La prima mensual no puede ser menor a 0", nameof(prima));
            PrimaMensual = prima;
        }

        public void AfiliarClienteAPoliza(Cliente cliente)
        {
            if (cliente is null)
                throw new ArgumentNullException(nameof(cliente));

            // Ya afiliado?
            if (Clientes.Any(c => c.Id == cliente.Id))
                throw new InvalidOperationException(
                    $"El cliente {cliente.Id} ya está afiliado a la póliza {NumeroPoliza}.");

            // Agrega al cliente a esta póliza
            Clientes.Add(cliente);

            // Sincroniza la navegación inversa
            cliente.AsignarSeguroSalud(this);
        }
    }
}