namespace ModuloClientes.Core.Models
{
    public class Empresa
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public string EIN { get; private set; }             // Employer Identification Number
        public string Direccion { get; private set; }
        public string Telefono { get; private set; }
        public string CorreoContacto { get; private set; }
        public DateTime FechaConstitucion { get; private set; }

        // Relación con Clientes
        public ICollection<EmpresaCliente> Clientes { get; private set; } = new List<EmpresaCliente>();

        // Constructor vacio:
        private Empresa() 
        {
            #pragma warning disable CS8618
            #pragma warning restore CS8618
        }

        public Empresa(
            string nombre,
            string ein,
            string direccion,
            string telefono,
            string correoContacto,
            DateTime fechaConstitucion)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre es obligatorio", nameof(nombre));
            if (string.IsNullOrWhiteSpace(ein))
                throw new ArgumentException("El EIN es obligatorio", nameof(ein));
            if (string.IsNullOrWhiteSpace(direccion))
                throw new ArgumentException("La direccion es obligatoria", nameof(direccion));
            if (string.IsNullOrWhiteSpace(telefono))
                throw new ArgumentException("El telefono es obligatorio", nameof(telefono));
            if (string.IsNullOrWhiteSpace(correoContacto))
                throw new ArgumentException("El correo de contacto es obligatorio", nameof(correoContacto));

            Nombre = nombre;
            EIN = ein;
            Direccion = direccion;
            Telefono = telefono;
            CorreoContacto = correoContacto;
            FechaConstitucion = fechaConstitucion;
        }


        public void CambiarNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede estar vacio", nameof(nombre));
            Nombre = nombre;
        }

        public void CambiarEin(string ein)
        {
            if (string.IsNullOrWhiteSpace(ein))
                throw new ArgumentException("El EIN no puede estar vacio", nameof(ein));
            EIN = ein;
        }

        public void CambiarDireccion(string nuevaDireccion)
        {
            if (string.IsNullOrWhiteSpace(nuevaDireccion))
                throw new ArgumentException("La dirección no puede estar vacía", nameof(nuevaDireccion));
            Direccion = nuevaDireccion;
        }

        public void CambiarTelefono(string nuevoTelefono)
        {
            if(string.IsNullOrWhiteSpace(nuevoTelefono))
                throw new ArgumentException("La direccion nueva no puede estar vacia", nameof(nuevoTelefono));
            this.Telefono = nuevoTelefono;
        }

        public void CambiarCorreo(string nuevoCorreo)
        {
            if(string.IsNullOrWhiteSpace(nuevoCorreo))
                throw new ArgumentException("El correo no puede estar vacio", nameof(nuevoCorreo));
            this.CorreoContacto = nuevoCorreo;
        }

        public void CambiarFechaDeConstitucion(DateTime fecha)
        {
            if(fecha > DateTime.Now)
                throw new ArgumentException("La fecha de constitucion de pude ser superior a la actual", nameof(fecha));
            FechaConstitucion = fecha;
        } 
    }
}