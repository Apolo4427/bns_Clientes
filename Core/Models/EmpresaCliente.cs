namespace ModuloClientes.Core.Models
{
    public class EmpresaCliente
    {
        public int ClienteId { get; private set; }
        public Cliente Cliente { get; private set; }
        public int EmpresaId { get; private set; }
        public Empresa Empresa { get; private set; }
        public string Rol { get; private set; }  // ej. "Dueño", "Socio"
        public DateTime FechaVinculacion { get; private set; }

        private EmpresaCliente()
        {
#pragma warning disable CS8618
#pragma warning restore CS8618
        }

        public EmpresaCliente(Cliente cliente, Empresa empresa, string rol, DateTime fechaVinculacion)
        {
            if (string.IsNullOrWhiteSpace(rol))
                throw new ArgumentException("El rol es obligatorio", nameof(rol));
            Cliente = cliente;
            ClienteId = Cliente.Id;
            Empresa = empresa;
            EmpresaId = empresa.Id;
            Rol = rol;
            FechaVinculacion = fechaVinculacion;
        }

        public void CambiarRol(string rol)
        {
            if(string.IsNullOrWhiteSpace(rol))
                throw new ArgumentException("El rol no puede estar vacio", nameof(rol));
            Rol = rol;
        }

        public void CambiarFechaDeVinculacion(DateTime fecha)
        {
            if(fecha > DateTime.Now)
                throw new ArgumentException("La fecha de vinculacion no puede ser superior a la fecha actual", nameof(fecha));
            FechaVinculacion = fecha;
        }
    }
}
