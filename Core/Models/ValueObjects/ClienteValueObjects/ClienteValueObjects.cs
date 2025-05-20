using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using ModuloClientes.Core.Models.ValueObjects.ValueObjectsExceptions.ClienteValueObjectsExceptions;

namespace ModuloClientes.Core.Models.ValueObjects.ClienteValueObjects
{
    public sealed class Name : IEquatable<Name>, IComparable<Name>
    {
        public string Value { get; }
        private const int MaxLength = 100;
        private const int MinLength = 2;
        private static readonly Regex NameRegex = new Regex(
            @"^[\p{L}\s'-]+$",
            RegexOptions.Compiled | RegexOptions.CultureInvariant);
        public Name(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El nombre es obligatorio", nameof(value));

            var trimmedValue = value.Trim();

            if (trimmedValue.Length < MinLength)
                throw new ArgumentException($"El nombre debe tener al menos {MinLength} caracteres", nameof(value));

            if (trimmedValue.Length > MaxLength)
                throw new ArgumentException($"El nombre no puede exceder {MaxLength} caracteres", nameof(value));

            if (!NameRegex.IsMatch(trimmedValue))
                throw new ArgumentException("El nombre contiene caracteres inválidos", nameof(value));

            Value = trimmedValue;
        }
        public override string ToString() => Value;
        public override bool Equals(object obj) => obj is Name other && Equals(other);
        public bool Equals([AllowNull] Name other) => other != null && Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);
        public override int GetHashCode() => Value.GetHashCode(StringComparison.OrdinalIgnoreCase);
        public int CompareTo([AllowNull] Name other) => string.Compare(Value, other?.Value, StringComparison.OrdinalIgnoreCase);
        public static bool operator ==(Name left, Name right) => left?.Equals(right) ?? right is null;
        public static bool operator !=(Name left, Name right) => !(left == right);
    }

    public sealed class Surname : IEquatable<Surname>, IComparable<Surname>
    {
        public string Value { get; }
        private const int MaxLength = 100;
        private const int MinLength = 2;
        private static readonly Regex SurnameRegex = new Regex(
            @"^[\p{L}\s'-]+$",
            RegexOptions.Compiled | RegexOptions.CultureInvariant
        );

        public Surname(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value), "El apellido no puede estar vacío");

            var trimmedValue = value.Trim();

            if (trimmedValue.Length < MinLength)
                throw new ArgumentException($"El apellido debe tener al menos {MinLength} caracteres", nameof(value));

            if (trimmedValue.Length > MaxLength)
                throw new ArgumentException($"El apellido no puede exceder {MaxLength} caracteres", nameof(value));

            if (!SurnameRegex.IsMatch(trimmedValue))
                throw new ArgumentException("El apellido contiene caracteres inválidos", nameof(value));

            Value = trimmedValue;
        }

        public override string ToString() => Value;
        public override bool Equals(object obj) => obj is Surname other && Equals(other);
        public bool Equals([AllowNull] Surname other) => other != null && Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);
        public override int GetHashCode() => Value.GetHashCode(StringComparison.OrdinalIgnoreCase);
        public int CompareTo([AllowNull] Surname other) => string.Compare(Value, other?.Value, StringComparison.OrdinalIgnoreCase);
        public static bool operator ==(Surname left, Surname right) => left?.Equals(right) ?? right is null;
        public static bool operator !=(Surname left, Surname right) => !(left == right);

    }

    public sealed class Email : IEquatable<Email>, IComparable<Email>
    {
        public string Value { get; }
        private string[] _parts;
        private const int MaxLength = 254;
        private static readonly Regex EmailRegex = new Regex(
            @"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$",
            RegexOptions.IgnoreCase | RegexOptions.Compiled,
            TimeSpan.FromMilliseconds(250));

        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value), "El correo electrónico no puede estar vacío");

            var trimmedValue = value.Trim().ToLowerInvariant();

            if (trimmedValue.Length > MaxLength)
                throw new ArgumentException($"El correo electrónico no puede exceder {MaxLength} caracteres", nameof(value));

            if (!EmailRegex.IsMatch(trimmedValue))
                throw new ArgumentException("Formato de correo electrónico inválido", nameof(value));

            var domain = trimmedValue.Split('@')[1];

            if (domain.Contains("..") || domain.StartsWith(".") || domain.EndsWith("."))
                throw new InvalidEmailException("El dominio del correo electrónico no es válido", nameof(value));

            Value = trimmedValue;
        }

        private string[] Parts => _parts ??= Value.Split('@');
        public string LocalPart => Parts[0];
        public string Domain => Parts[1];

        public override string ToString() => Value;
        public override bool Equals(object obj) => obj is Email other && Equals(other);
        public bool Equals([AllowNull] Email other) => other != null && Value == other.Value;
        public override int GetHashCode() => Value.GetHashCode();
        public int CompareTo([AllowNull] Email other) => string.Compare(Value, other?.Value, StringComparison.Ordinal);
        public static bool operator ==(Email left, Email right) => left?.Equals(right) ?? right is null;
        public static bool operator !=(Email left, Email right) => !(left == right);
    }

    public sealed class Phone : IEquatable<Phone>, IComparable<Phone>
    {
        public string Value { get; }
        private const int MinLength = 7;
        private const int MaxLength = 15;
        private static readonly Regex PhoneRegex = new Regex(
            @"^\+?[0-9\s\-\(\)]+$",
            RegexOptions.Compiled
        );

        public Phone(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value), "El teléfono no puede estar vacío");

            var digitsOnly = new string(value.Where(char.IsDigit).ToArray());

            if (digitsOnly.Length < MinLength)
                throw new ArgumentException($"El teléfono debe tener al menos {MinLength} dígitos", nameof(value));

            if (digitsOnly.Length > MaxLength)
                throw new ArgumentException($"El teléfono no puede exceder {MaxLength} dígitos", nameof(value));

            if (!PhoneRegex.IsMatch(value))
                throw new ArgumentException("Formato de teléfono inválido", nameof(value));

            Value = digitsOnly;
        }

        public string Formatted => $"+{Value}";
        public override string ToString() => Value;
        public override bool Equals(object obj) => obj is Phone other && Equals(other);
        public bool Equals([AllowNull] Phone other) => other != null && Value.Equals(other.Value);
        public override int GetHashCode() => Value.GetHashCode();
        public int CompareTo([AllowNull] Phone other) => string.Compare(Value, other?.Value, StringComparison.Ordinal);
        public static bool operator ==(Phone left, Phone right) => left?.Equals(right) ?? right is null;
        public static bool operator !=(Phone left, Phone right) => !(left == right);
    }

    public sealed class SSN : IEquatable<SSN>, IComparable<SSN>
    {
        public string Value { get; }
        private const int ExactLength = 9;
        private static readonly Regex SsnRegex = new Regex(@"^\d{3}-\d{2}-\d{4}$");

        public SSN(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value), "El SSN no puede estar vacío");

            var digitsOnly = new string(value.Where(char.IsDigit).ToArray());

            if (digitsOnly.Length != ExactLength)
                throw new ArgumentException($"El SSN debe tener exactamente {ExactLength} dígitos", nameof(value));

            if (!SsnRegex.IsMatch(value) && digitsOnly.Length != value.Length)
                throw new ArgumentException("Formato de SSN inválido. Use XXX-XX-XXXX o 9 dígitos", nameof(value));

            Value = digitsOnly;
        }

        public string Formatted => $"{Value.Substring(0, 3)}-{Value.Substring(3, 2)}-{Value.Substring(5, 4)}";
        public override string ToString() => Formatted;
        public override bool Equals(object obj) => obj is SSN other && Equals(other);
        public bool Equals([AllowNull] SSN other) => other != null && Value.Equals(other.Value);
        public override int GetHashCode() => Value.GetHashCode();
        public int CompareTo([AllowNull] SSN other) => string.Compare(Value, other?.Value, StringComparison.Ordinal);
        public static bool operator ==(SSN left, SSN right) => left?.Equals(right) ?? right is null;
        public static bool operator !=(SSN left, SSN right) => !(left == right);
    }

    public sealed class Address
    {
        public string Value { get; }
        private const int MaxLength = 200;
        private const int MinLength = 5;

        public Address(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value), "La dirección no puede estar vacía");

            if (value.Length < MinLength)
                throw new ArgumentException($"La dirección debe tener al menos {MinLength} caracteres", nameof(value));

            if (value.Length > MaxLength)
                throw new ArgumentException($"La dirección no puede exceder {MaxLength} caracteres", nameof(value));

            Value = value.Trim();
        }

        public override string ToString() => Value;
        public override bool Equals(object obj) => obj is Address other && Equals(other);
        public bool Equals([AllowNull] Address other) => other != null && Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);
        public override int GetHashCode() => Value.GetHashCode(StringComparison.OrdinalIgnoreCase);
        public int CompareTo([AllowNull] Address other) => string.Compare(Value, other?.Value, StringComparison.OrdinalIgnoreCase);
        public static bool operator ==(Address left, Address right) => left?.Equals(right) ?? right is null;
        public static bool operator !=(Address left, Address right) => !(left == right);
    }
}