using ModuloClientes.Core.Enums;
using ModuloClientes.Core.Models.ValueObjects.ClienteValueObjects;

namespace ModuloClientes.Core.Models
{
    public class Cliente
    {
        public int Id { get; private set; } // Clave primaria

        // Datos personales
        public Name Nombre { get; private set; }
        public Surname Apellido { get; private set; }
        public Email Correo { get; private set; }
        public Phone Telefono { get; private set; }
        public DateTime FechaNacimiento { get; private set; }

        // Datos fiscales
        public MaritalStatus EstadoCivil { get; private set; }
        public TaxStatus EstadoTributario { get; private set; }
        public SSN SocialSecurityNumber { get; private set; }
        public ICollection<string> Oficios { get; private set; } = new List<string>();

        // Estado dentro del sistema: Prospecto o Activo
        public EstadoCliente Estado { get; private set; }

         // Relaciones con otros clientes (cónyuges, dependientes, etc.)
        public ICollection<ClienteRelacion> Relaciones { get; private set; } = new List<ClienteRelacion>();

        // Dirección
        public Address Direccion { get; private set; }

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
            Name nombre,
            Surname apellido,
            Email correo,
            Phone telefono,
            DateTime fechaNacimiento,
            MaritalStatus estadoCivil,
            TaxStatus estadoTributario,
            SSN socialSecurityNumber,
            Address direccion)
        {
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            Apellido = apellido ?? throw new ArgumentNullException(nameof(apellido));
            Correo = correo ?? throw new ArgumentNullException(nameof(correo));
            Telefono = telefono ?? throw new ArgumentNullException(nameof(telefono));
            FechaNacimiento = fechaNacimiento;
            EstadoCivil = estadoCivil;
            EstadoTributario = estadoTributario;
            SocialSecurityNumber = socialSecurityNumber ?? throw new ArgumentNullException(nameof(socialSecurityNumber));
            Direccion = direccion ?? throw new ArgumentNullException(nameof(direccion));
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
            // TODO: Implementar cambio de estado del cliente
            Estado = EstadoCliente.Prospecto; // por defecto
        }

        // Metodos de dominio

        public void MarcarActivo() => Estado = EstadoCliente.Activo;
        public void MarcarProspecto() => Estado = EstadoCliente.Prospecto;

                // Métodos de actualización robustos al nivel de un senior
        public void CambiarNombre(Name nuevoNombre)
        {
            if (nuevoNombre is null)
                throw new ArgumentNullException(nameof(nuevoNombre));

            if (nuevoNombre.Equals(Nombre))
                return;

            Nombre = nuevoNombre;
        }

        public void CambiarApellido(Surname nuevoApellido)
        {
            if (nuevoApellido is null)
                throw new ArgumentNullException(nameof(nuevoApellido));

            if (nuevoApellido.Equals(Apellido))
                return;

            Apellido = nuevoApellido;
        }

        public void CambiarCorreo(Email nuevoCorreo)
        {
            if (nuevoCorreo is null)
                throw new ArgumentNullException(nameof(nuevoCorreo));

            if (nuevoCorreo.Equals(Correo))
                return;

            Correo = nuevoCorreo;
        }

        public void CambiarTelefono(Phone nuevoTelefono)
        {
            if (nuevoTelefono is null)
                throw new ArgumentNullException(nameof(nuevoTelefono));

            if (nuevoTelefono.Equals(Telefono))
                return;

            Telefono = nuevoTelefono;
        }

        public void CambiarFechaNacimiento(DateTime nuevaFechaNacimiento)
        {
            if (nuevaFechaNacimiento == default)
                throw new ArgumentException("Fecha de nacimiento inválida.", nameof(nuevaFechaNacimiento));

            if (nuevaFechaNacimiento > DateTime.Today)
                throw new ArgumentException("La fecha de nacimiento no puede ser posterior a hoy.", nameof(nuevaFechaNacimiento));

            if (nuevaFechaNacimiento.Equals(FechaNacimiento))
                return;

            FechaNacimiento = nuevaFechaNacimiento;
        }

        public void CambiarDireccion(Address nuevaDireccion)
        {
            if (nuevaDireccion is null)
                throw new ArgumentNullException(nameof(nuevaDireccion));

            if (nuevaDireccion.Equals(Direccion))
                return;

            Direccion = nuevaDireccion;
        }

        public void CambiarSSN(SSN nuevoSSN)
        {
            if (nuevoSSN is null)
                throw new ArgumentNullException(nameof(nuevoSSN));

            if (nuevoSSN.Equals(SocialSecurityNumber))
                return;

            SocialSecurityNumber = nuevoSSN;
        }

        public void CambiarEstadoCivil(MaritalStatus nuevoEstadoCivil)
        {
            if (!Enum.IsDefined(typeof(MaritalStatus), nuevoEstadoCivil))
                throw new ArgumentException("Estado civil inválido.", nameof(nuevoEstadoCivil));

            if (nuevoEstadoCivil == EstadoCivil)
                return;

            EstadoCivil = nuevoEstadoCivil;
        }

        public void CambiarEstadoTributario(TaxStatus nuevoEstadoTributario)
        {
            if (!Enum.IsDefined(typeof(TaxStatus), nuevoEstadoTributario))
                throw new ArgumentException("Estado tributario inválido.", nameof(nuevoEstadoTributario));

            if (nuevoEstadoTributario == EstadoTributario)
                return;

            EstadoTributario = nuevoEstadoTributario;
        }

        public int Edad
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - FechaNacimiento.Year;
                if (FechaNacimiento.Date > today.AddYears(-age)) age--;
                return age;
            }
        }
        // TODO: IMPLEMENTAR HANDLER PARA OFICIOS
        public void AgregarOficio(string nuevoOficio)
        {
            if (string.IsNullOrWhiteSpace(nuevoOficio))
                throw new ArgumentException("No se pueden agregar oficios vacios", nameof(nuevoOficio));
            Oficios.Add(nuevoOficio.ToLower());
        }

        public void EliminarOficio(string oficio)
        {
            if (string.IsNullOrWhiteSpace(oficio) || !Oficios.Contains(oficio))
                throw new ArgumentNullException("El oficio esta vacio o nulo, o se esta intentando eliminar un oficio que el cliente no tiene",
                    nameof(oficio));            
            Oficios.Remove(oficio.ToLower());
            
        }

        public void ReemplazarOficios(IEnumerable<string> nuevosOficios)
        {
            if(nuevosOficios is null)
                throw new ArgumentException("No se pueden introducir oficios nulos", nameof(nuevosOficios));
            Oficios.Clear();
            foreach (var o in nuevosOficios)
                AgregarOficio(o.ToLower());
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