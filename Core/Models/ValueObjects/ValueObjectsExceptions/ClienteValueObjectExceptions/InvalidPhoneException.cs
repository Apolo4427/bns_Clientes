namespace ModuloClientes.Core.Models.ValueObjects.ValueObjectsExceptions.ClienteValueObjectsExceptions
{
    public class InvalidPhoneException : ArgumentException
    {
        public InvalidPhoneException(){ }
        public InvalidPhoneException(string message)                  : base(message)               { }
        public InvalidPhoneException(string message, string paramName) : base(message, paramName)   { }
        public InvalidPhoneException(string message, Exception inner)  : base(message, inner) { }
    }
}