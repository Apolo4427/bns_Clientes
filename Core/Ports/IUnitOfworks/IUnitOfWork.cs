namespace ModuloClientes.Core.Ports.IUnitOfWorks
{
    /// <summary>
    /// Unidad de trabajo para agrupar operaciones de BD en una sola transacción.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Persiste los cambios pendientes en el contexto.
        /// </summary>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}