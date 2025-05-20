namespace ModuloClientes.Core.Models.ValueObjects.ValueObjectsExceptions.ClienteValueObjectsExceptions
{
    public sealed class InvalidEmailException : ArgumentException
    {
        public InvalidEmailException(string message, string paramName)
            : base (message, paramName)
        {

        }
    }
}