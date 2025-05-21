namespace ModuloClientes.Core.Models.ValueObjects.ValueObjectsExceptions.ClienteValueObjectsExceptions
{
    [Serializable]
    public class InvalidAddressException : ArgumentException
    {
        public InvalidAddressException() { }
        public InvalidAddressException(string message)                  : base(message)             { }
        public InvalidAddressException(string message, string paramName) : base(message, paramName){ }
        public InvalidAddressException(string message, Exception inner)  : base(message, inner)    { }
    }
}