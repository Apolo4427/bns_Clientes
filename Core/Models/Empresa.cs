using ModuloClientes.Core.Models.ValueObjects.ClienteValueObjects;
using ModuloClientes.Core.Models.ValueObjects.EmpresaValueObjects;

namespace ModuloClientes.Core.Models
{
    public class Empresa
    {
       public int Id { get; private set; }

        // Value Objects
        public CompanyName Nombre { get; private set; }
        public EIN Ein { get; private set; }
        public Address Direccion { get; private set; }
        public Phone Telefono { get; private set; }
        public Email CorreoContacto { get; private set; }
        public DateTime FechaConstitucion { get; private set; }

        // Relación con Clientes
        public ICollection<EmpresaCliente> Clientes { get; private set; } = new List<EmpresaCliente>();

        // Constructor para EF
        private Empresa() { }

        // Constructor de dominio
        public Empresa(
            CompanyName nombre,
            EIN ein,
            Address direccion,
            Phone telefono,
            Email correoContacto,
            DateTime fechaConstitucion)
        {
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            Ein = ein ?? throw new ArgumentNullException(nameof(ein));
            Direccion = direccion ?? throw new ArgumentNullException(nameof(direccion));
            Telefono = telefono ?? throw new ArgumentNullException(nameof(telefono));
            CorreoContacto = correoContacto ?? throw new ArgumentNullException(nameof(correoContacto));

            if (fechaConstitucion == default)
                throw new ArgumentException("Fecha de constitución inválida.", nameof(fechaConstitucion));
            if (fechaConstitucion > DateTime.Today)
                throw new ArgumentException("La fecha de constitución no puede ser futura.", nameof(fechaConstitucion));

            FechaConstitucion = fechaConstitucion;
        }

        // Métodos de actualización (nivel senior)
        public void CambiarNombre(CompanyName nuevoNombre)
        {
            if (nuevoNombre is null)
                throw new ArgumentNullException(nameof(nuevoNombre));
            if (nuevoNombre.Equals(Nombre))
                return;
            Nombre = nuevoNombre;
        }

        public void CambiarEIN(EIN nuevoEin)
        {
            if (nuevoEin is null)
                throw new ArgumentNullException(nameof(nuevoEin));
            if (nuevoEin.Equals(Ein))
                return;
            Ein = nuevoEin;
        }

        public void CambiarDireccion(Address nuevaDireccion)
        {
            if (nuevaDireccion is null)
                throw new ArgumentNullException(nameof(nuevaDireccion));
            if (nuevaDireccion.Equals(Direccion))
                return;
            Direccion = nuevaDireccion;
        }

        public void CambiarTelefono(Phone nuevoTelefono)
        {
            if (nuevoTelefono is null)
                throw new ArgumentNullException(nameof(nuevoTelefono));
            if (nuevoTelefono.Equals(Telefono))
                return;
            Telefono = nuevoTelefono;
        }

        public void CambiarCorreoContacto(Email nuevoCorreo)
        {
            if (nuevoCorreo is null)
                throw new ArgumentNullException(nameof(nuevoCorreo));
            if (nuevoCorreo.Equals(CorreoContacto))
                return;
            CorreoContacto = nuevoCorreo;
        }

        public void CambiarFechaConstitucion(DateTime nuevaFechaConstitucion)
        {
            if (nuevaFechaConstitucion == default)
                throw new ArgumentException("Fecha de constitución inválida.", nameof(nuevaFechaConstitucion));
            if (nuevaFechaConstitucion > DateTime.Today)
                throw new ArgumentException("La fecha de constitución no puede ser futura.", nameof(nuevaFechaConstitucion));
            if (nuevaFechaConstitucion.Equals(FechaConstitucion))
                return;
            FechaConstitucion = nuevaFechaConstitucion;
        }
    }
}