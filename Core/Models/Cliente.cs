using ModuloClientes.Core.Enums;

namespace ModuloClientes.Core.Models
{
    public class Cliente
    {
        public int Id { get; private set; } // Clave primaria

        // Datos personales
        public string Nombre { get; private set; }
        public string Apellido { get; private set; }
        public int Edad 
            => CalcularEdad(FechaNacimiento);
        public string Correo { get; private set; }
        public string Telefono { get; private set; }
        public DateTime FechaNacimiento { get; private set; }

        // Datos fiscales
        public string EstadoCivil { get; private set; }
        public string EstadoTributario { get; private set; }
        public string SocialSecurityNumber { get; private set; }
        public ICollection<string> Oficios { get; private set; } = new List<string>();

        // Estado dentro del sistema: Prospecto o Activo
        public EstadoCliente Estado { get; private set; }

         // Relaciones con otros clientes (cónyuges, dependientes, etc.)
        public ICollection<ClienteRelacion> Relaciones { get; private set; } = new List<ClienteRelacion>();

        // Dirección
        public string Direccion { get; private set; }

        // Relación con Empresas (dueño o socio)
        public ICollection<EmpresaCliente> Empresas { get; private set; } = new List<EmpresaCliente>();

        // Relacion con SeguroSalud
        public int? SeguroSaludId { get; private set; }
        public SeguroSalud? SeguroSalud { get; private set; }

        // Constructor vacio
        private Cliente()
        {
            #pragma warning disable CS8618
            #pragma warning restore CS8618
        }

        // Constructor para garantizar invariantes
        public Cliente(
            string nombre,
            string apellido,
            string correo,
            string telefono,
            DateTime fechaNacimiento,
            string estadoCivil,
            string estadoTributario,
            string socialSecurityNumber,
            string direccion)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre es obligatorio", nameof(nombre));
            if (string.IsNullOrWhiteSpace(apellido))
                throw new ArgumentException("El apellido es obligatorio", nameof(apellido));
            if (string.IsNullOrWhiteSpace(telefono))
                throw new ArgumentException("El telefono es obligatorio", nameof(telefono));
            if (string.IsNullOrWhiteSpace(correo))
                throw new ArgumentException("El correo es obligatorio", nameof(correo));
            if (string.IsNullOrWhiteSpace(socialSecurityNumber))
                throw new ArgumentException("EL SSN es obligatorio", nameof(socialSecurityNumber));
            if (string.IsNullOrWhiteSpace(direccion))
                throw new ArgumentException("La direccion es obligatorioa", nameof(direccion));
            if (string.IsNullOrWhiteSpace(estadoTributario))
                throw new ArgumentException("El estado tributario es obligatorio", nameof(estadoTributario));
            if (fechaNacimiento == default  ||
                fechaNacimiento < new DateTime(1900, 1, 1) ||
                fechaNacimiento > DateTime.Today)
                throw new ArgumentException("La fecha de nacimiento debe estar entre 1900 y hoy", nameof(fechaNacimiento));

            Nombre = nombre;
            Apellido = apellido;
            Correo = correo;
            Telefono = telefono;
            FechaNacimiento = fechaNacimiento;
            EstadoCivil = estadoCivil;
            EstadoTributario = estadoTributario;
            SocialSecurityNumber = socialSecurityNumber;
            Direccion = direccion;
            Estado = EstadoCliente.Activo; // por defecto
        }

        // Metodos de dominio

        public void MarcarActivo() => Estado = EstadoCliente.Activo;
        public void MarcarProspecto() => Estado = EstadoCliente.Prospecto;

        public void CambiarSocialSecurityNumber(string nuevoSocial)
        {
            if (string.IsNullOrWhiteSpace(nuevoSocial))
                throw new ArgumentException("El Social Security Number no puede estar vacio");
            SocialSecurityNumber = nuevoSocial;
        }

        public void CambiarCorreo(string nuevoCorreo)
        {
            if (string.IsNullOrWhiteSpace(nuevoCorreo))
                throw new ArgumentException("El correo no puede estar vacío", nameof(nuevoCorreo));
            this.Correo = nuevoCorreo;
        }

        public void CambiarTelefono(string nuevoTelefono)
        {
            if (string.IsNullOrWhiteSpace(nuevoTelefono))
                throw new ArgumentException("El telefono no puede estar vacio", nameof(nuevoTelefono));
            this.Telefono = nuevoTelefono;
        }

        public void CambiarFechaDeNacimiento(DateTime fechaDeNacimiento)
        {
            if (fechaDeNacimiento > DateTime.Now)
                throw new ArgumentException("La fecha de nacimiento no puede ser posterior a la fecha actual", nameof(fechaDeNacimiento));
            FechaNacimiento = fechaDeNacimiento;
        }

        public void CambiarDireccion(string nuevaDireccion)
        {
            if (string.IsNullOrWhiteSpace(nuevaDireccion))
                throw new ArgumentException("La direccion no puede esta vacia", nameof(nuevaDireccion));
            this.Direccion = nuevaDireccion;
        }
        public void CambiarNombre(string nuevoNombre)
        {
            if (string.IsNullOrWhiteSpace(nuevoNombre))
                throw new ArgumentException("El nombre no puede estar vacío", nameof(nuevoNombre));
            Nombre = nuevoNombre;
        }

        public void CambiarApellido(string nuevoApellido)
        {
            if (string.IsNullOrWhiteSpace(nuevoApellido))
                throw new ArgumentException("El apellido no puede estar vacío", nameof(nuevoApellido));
            Apellido = nuevoApellido;
        }

        public void CambiarEstadoCivil(string nuevoEstadoCivil)
        {
            if (string.IsNullOrWhiteSpace(nuevoEstadoCivil))
                throw new ArgumentException("El estado civil no puede estar vacío", nameof(nuevoEstadoCivil));
            EstadoCivil = nuevoEstadoCivil;
        }

        public void CambiarEstadoTributario(string nuevoEstadoTributario)
        {
            if (string.IsNullOrWhiteSpace(nuevoEstadoTributario))
                throw new ArgumentException("El estado tributario no puede estar vacío", nameof(nuevoEstadoTributario));
            EstadoTributario = nuevoEstadoTributario;
        }

        public int CalcularEdad(DateTime fechaNacimiento)
        {
            var hoy = DateTime.Today;
            var edad = hoy.Year - fechaNacimiento.Year;

            // Si el cumpleaños no ha ocurrido este año, restar 1
            if (fechaNacimiento.Date > hoy.AddYears(-edad))
                edad--;

            return edad;
        }

        public void AgregarOficio(string nuevoOficio)
        {
            if (string.IsNullOrWhiteSpace(nuevoOficio))
                throw new ArgumentException("No se pueden agregar oficios vacios", nameof(nuevoOficio));
            Oficios.Add(nuevoOficio);
        }

        public void EliminarOficio(string oficio)
        {
            if (string.IsNullOrWhiteSpace(oficio))
                throw new ArgumentNullException("El oficio esta vacio o nulo", nameof(oficio));
            Oficios.Remove(oficio);
        }

        public void VincularEmpresa(Empresa empresa, string rol, DateTime fecha)
        {
            if (this.Empresas.Any(e => e.EmpresaId == empresa.Id))
                throw new InvalidOperationException($"El cliente ya está vinculado a la empresa {empresa.Id}");
            // TODO: buscar la empresa con el Empresaid y asi asignar lo en constructor
            // tambien buscar el objeto 
            var vinculo = new EmpresaCliente(this, empresa, rol, fecha);
            Empresas.Add(vinculo);
        }

        public void DesvincularEmpresa(int empresaId)
        {
            var ec = Empresas.FirstOrDefault(x => x.EmpresaId == empresaId)
                ?? throw new KeyNotFoundException($"No existe vínculo con la empresa {empresaId}");
            Empresas.Remove(ec);
        }

        public void AsignarSeguroSalud(SeguroSalud seguro)
        {
            if (seguro is null)
                throw new ArgumentNullException(nameof(seguro));

            // Si ya tiene una póliza distinta, podríamos validar aquí o reemplazar
            SeguroSalud = seguro;
            SeguroSaludId = seguro.Id;
        }

    }
}