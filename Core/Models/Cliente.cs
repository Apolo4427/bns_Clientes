using ModuloClientes.Core.Enums;
using ModuloClientes.Core.Models.ValueObjects.ClienteValueObjects;

namespace ModuloClientes.Core.Models
{
    public class Cliente
    {
        public Guid Id { get; private set; } // Clave primaria

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
        public ICollection<Oficio> Oficios { get; private set; } = new List<Oficio>();

        // Estado dentro del sistema: Prospecto o Activo
        public EstadoCliente Estado { get; private set; }

        // Relaciones con otros clientes (cónyuges, dependientes, etc.)
        public ICollection<ClienteRelacion> Relaciones { get; private set; } = new List<ClienteRelacion>();

        // Dirección
        public Address Direccion { get; private set; }

        // Relación con Empresas (dueño o socio)
        public ICollection<EmpresaCliente> Empresas { get; private set; } = new List<EmpresaCliente>();

        // Relacion con SeguroSalud
        public Guid? SeguroSaludId { get; private set; }
        public SeguroSalud? SeguroSalud { get; private set; }
        public byte[] RowVersion { get; private set; }

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
            // guid secuensial asignado por la base de datos sql
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            Apellido = apellido ?? throw new ArgumentNullException(nameof(apellido));
            Correo = correo ?? throw new ArgumentNullException(nameof(correo));
            Telefono = telefono ?? throw new ArgumentNullException(nameof(telefono));
            SocialSecurityNumber = socialSecurityNumber ?? throw new ArgumentNullException(nameof(socialSecurityNumber));
            Direccion = direccion ?? throw new ArgumentNullException(nameof(direccion));
            if (fechaNacimiento == default ||
                fechaNacimiento < new DateTime(1900, 1, 1) ||
                fechaNacimiento > DateTime.Today)
                throw new ArgumentException("La fecha de nacimiento debe estar entre 1900 y hoy", nameof(fechaNacimiento));
            FechaNacimiento = fechaNacimiento;
            EstadoCivil = estadoCivil;
            EstadoTributario = estadoTributario;
            // TODO: Implementar cambio de estado del cliente
            Estado = EstadoCliente.Prospecto; // por defecto
        }

        // Metodos de dominio

        // public void MarcarActivo() => Estado = EstadoCliente.Activo;
        // public void MarcarProspecto() => Estado = EstadoCliente.Prospecto;
        public void CambiarEstado(EstadoCliente nuevoEstado)
        {
            Estado = nuevoEstado;
        }

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
        public void AgregarOficio(Oficio nuevoOficio)
        {
            if (nuevoOficio is null)
                throw new ArgumentException("No se pueden agregar oficios vacios", nameof(nuevoOficio));

            if (Oficios.Any(o => o.EsMismoOficio(nuevoOficio)))
                throw new InvalidOperationException($"El oficio '{nuevoOficio}' ya existe");

            Oficios.Add(nuevoOficio);
        }

        public void EliminarOficio(Oficio oficio)
        {
            if (oficio is null)
                throw new ArgumentNullException("El oficio esta vacio o nulo",
                    nameof(oficio));

            var oficioExistente = Oficios.FirstOrDefault(o => o.EsMismoOficio(oficio))
                ?? throw new KeyNotFoundException("El oficio no existe en este cliente");

            Oficios.Remove(oficioExistente);
        }

        public void ReemplazarOficios(IEnumerable<Oficio> nuevosOficios)
        {
            if (nuevosOficios is null)
                throw new ArgumentException("No se pueden introducir oficios nulos", nameof(nuevosOficios));
            Oficios.Clear();
            foreach (var o in nuevosOficios.Distinct())
                AgregarOficio(o);
        }

        public void VincularEmpresa(Empresa empresa, string rol, DateTime fecha)
        {
            if (this.Empresas.Any(e => e.EmpresaId == empresa.Id))
                throw new InvalidOperationException($"El cliente ya está vinculado a la empresa {empresa.Id}");
            var vinculo = new EmpresaCliente(this, empresa, rol, fecha);
            Empresas.Add(vinculo);
        }

        public void DesvincularEmpresa(Guid empresaId)
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

        public void RemoverSeguroSalud()
        {
            SeguroSalud = null;
            SeguroSaludId = null;
        }

        /// <summary>
        /// Agrega una relación UNIDIRECCIONAL de este cliente hacia otro,
        /// aplicando todas las validaciones de negocio.
        /// </summary>
        internal void AgregarRelacionUnidireccional(Cliente clienteRelacionado, TipoRelacion tipo, bool esDependiente)
        {
            if (clienteRelacionado is null)
                throw new ArgumentNullException(nameof(clienteRelacionado),
                    "El cliente que será relacionado no puede ser nulo.");

            if (clienteRelacionado.Id == this.Id)
                throw new InvalidOperationException("Un cliente no puede relacionarse consigo mismo.");

            // Solo un cónyuge permitido
            if (tipo == TipoRelacion.Conyuge
                && Relaciones.Any(r => r.Tipo == TipoRelacion.Conyuge))
            {
                throw new InvalidOperationException(
                    "Ya existe un cónyuge registrado para este cliente.");
            }

            // Evita duplicados (mismo par ClienteId → RelacionadoId)
            if (Relaciones.Any(r => r.RelacionadoId == clienteRelacionado.Id))
            {
                throw new InvalidOperationException(
                    $"La relación con el cliente {clienteRelacionado.Id} ya existe.");
            }

            Relaciones.Add(new ClienteRelacion(this, clienteRelacionado, tipo, esDependiente));
        }

        /// <summary>
        /// Elimina la relación UNIDIRECCIONAL de este cliente hacia otro.
        /// </summary>
        public void EliminarRelacion(Guid relacionadoId)
        {
            var existente = Relaciones
                .FirstOrDefault(r => r.RelacionadoId == relacionadoId)
                ?? throw new KeyNotFoundException(
                    $"No se encontró una relación con el cliente {relacionadoId}.");

            Relaciones.Remove(existente);
        }

        /// <summary>
        /// Cambia el tipo de relación UNIDIRECCIONAL de este cliente hacia otro.
        /// </summary>
        public void CambiarTipoRelacion(Guid relacionadoId, TipoRelacion nuevoTipo)
        {
            var relacion = Relaciones
                .FirstOrDefault(r => r.RelacionadoId == relacionadoId)
                ?? throw new KeyNotFoundException(
                    $"No se encontró una relación con el cliente {relacionadoId}.");

            // Regla opcional: sólo un cónyuge
            if (nuevoTipo == TipoRelacion.Conyuge
                && Relaciones.Any(r => r.Tipo == TipoRelacion.Conyuge && r.RelacionadoId != relacionadoId))
            {
                throw new InvalidOperationException("Ya existe otro cónyuge registrado para este cliente.");
            }

            relacion.CambiarTipoRelacion(nuevoTipo);
        }

        /// <summary>
        /// Marca o desmarca al cliente relacionado como dependiente UNIDIRECCIONAL.
        /// </summary>
        public void CambiarDependenciaRelacion(Guid relacionadoId, bool esDependiente)
        {
            var relacion = Relaciones
                .FirstOrDefault(r => r.RelacionadoId == relacionadoId)
                ?? throw new KeyNotFoundException(
                    $"No se encontró una relación con el cliente {relacionadoId}.");

            relacion.CambiarDependencia(esDependiente);
        }

    }
}