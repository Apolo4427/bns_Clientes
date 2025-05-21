using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using ModuloClientes.Core.Models.ValueObjects.ValueObjectsExceptions.ClienteValueObjectsExceptions;

namespace ModuloClientes.Core.Models.ValueObjects.ClienteValueObjects
{
    /// </summary>
    /// nombre del cliente 
    /// </summary>
    public sealed class Name : IEquatable<Name>, IComparable<Name>
    {
        public string Value { get; }
        private const int MaxLength = 100;
        private const int MinLength = 2;
        private static readonly Regex NameRegex = new Regex(
            @"^[\p{L}\s'-]+$",
            RegexOptions.Compiled | RegexOptions.CultureInvariant,
            TimeSpan.FromMilliseconds(250)
        );
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

        public static Result<Name> TryCreate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Result<Name>.Failure("El nombre es obligatorio");

            var trimmedValue = value.Trim();

            if (trimmedValue.Length < MinLength)
                return Result<Name>.Failure($"El nombre debe tener al menos {MinLength} caracteres");

            if (trimmedValue.Length > MaxLength)
                return Result<Name>.Failure($"El nombre no puede exceder {MaxLength} caracteres");

            if (!NameRegex.IsMatch(trimmedValue))
                return Result<Name>.Failure("El nombre contiene caracteres inválidos");

            try
            {
                return Result<Name>.Success(new Name(trimmedValue));
            }
            catch (Exception ex)
            {
                return Result<Name>.Failure($"Error inesperado al crear el nombre: {ex.Message}");
            }
        }
    }
    /// </summary>
    /// apellido del cliente
    /// </summary>
    public sealed class Surname : IEquatable<Surname>, IComparable<Surname>
    {
        public string Value { get; }
        private const int MaxLength = 100;
        private const int MinLength = 2;
        private static readonly Regex SurnameRegex = new Regex(
            @"^[\p{L}\s'-]+$",
            RegexOptions.Compiled | RegexOptions.CultureInvariant,
            TimeSpan.FromMilliseconds(250)
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
        public static Result<Surname> TryCreate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Result<Surname>.Failure("El apellido no puede estar vacío");

            var trimmedValue = value.Trim();

            if (trimmedValue.Length < MinLength)
                return Result<Surname>.Failure($"El apellido debe tener al menos {MinLength} caracteres");

            if (trimmedValue.Length > MaxLength)
                return Result<Surname>.Failure($"El apellido no puede exceder {MaxLength} caracteres");

            if (!SurnameRegex.IsMatch(trimmedValue))
                return Result<Surname>.Failure("El apellido contiene caracteres inválidos");

            try
            {
                return Result<Surname>.Success(new Surname(trimmedValue));
            }
            catch (Exception ex)
            {
                return Result<Surname>.Failure($"Error inesperado al crear el apellido: {ex.Message}");
            }
        }

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

        //Para flujos masivos
        public static Result<Email> TryCreate(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return Result<Email>.Failure("El correo electrónico no puede estar vacío");

            var trimmedValue = email.Trim().ToLowerInvariant();

            if (trimmedValue.Length > MaxLength)
                return Result<Email>.Failure($"El correo electrónico no puede exceder {MaxLength} caracteres");

            if (!EmailRegex.IsMatch(trimmedValue))
                return Result<Email>.Failure("Formato de correo electrónico inválido");

            var parts = trimmedValue.Split('@');
            if (parts.Length != 2)
                return Result<Email>.Failure("El correo debe contener exactamente un @");

            var domain = parts[1];
            if (domain.Contains("..") || domain.StartsWith(".") || domain.EndsWith("."))
                return Result<Email>.Failure("El dominio del correo electrónico no es válido");

            try
            {
                return Result<Email>.Success(new Email(trimmedValue));
            }
            catch (Exception ex)
            {
                // Captura cualquier excepción no esperada durante la construcción
                return Result<Email>.Failure($"Error inesperado al crear el email: {ex.Message}");
            }
        }

    }

    public sealed class Phone : IEquatable<Phone>, IComparable<Phone>
    {
        public string Value { get; }
        private const int MinLength = 7;
        private const int MaxLength = 15;
        private static readonly Regex PhoneRegex = new Regex(
            @"^\+?[0-9\s\-\(\)]+$",
            RegexOptions.Compiled,
            TimeSpan.FromMilliseconds(250)
        );

        public Phone(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidPhoneException("El teléfono no puede estar vacío", nameof(value));

            var digitsOnly = new string(value.Where(char.IsDigit).ToArray());

            if (digitsOnly.Length < MinLength)
                throw new InvalidPhoneException($"El teléfono debe tener al menos {MinLength} dígitos", nameof(value));

            if (digitsOnly.Length > MaxLength)
                throw new InvalidPhoneException($"El teléfono no puede exceder {MaxLength} dígitos", nameof(value));

            if (!PhoneRegex.IsMatch(value))
                throw new InvalidPhoneException("Formato de teléfono inválido", nameof(value));

            if (digitsOnly.Length > 0 && !char.IsDigit(digitsOnly[0]))
                throw new InvalidPhoneException("El teléfono debe comenzar con un dígito", nameof(value));

            Value = digitsOnly;
        }

        public string Formatted => Value.Length > 10
            ? $"+{Value.Substring(0, Value.Length - 10)} {Value.Substring(Value.Length - 10, 3)}-{Value.Substring(Value.Length - 7, 3)}-{Value.Substring(Value.Length - 4)}"
            : $"+1 {Value.Substring(0, 3)}-{Value.Substring(3, 3)}-{Value.Substring(6)}";

        public override string ToString() => Value;
        public override bool Equals(object obj) => obj is Phone other && Equals(other);
        public bool Equals([AllowNull] Phone other) => other != null && Value == other.Value;
        public override int GetHashCode() => Value.GetHashCode();
        public int CompareTo([AllowNull] Phone other) => string.Compare(Value, other?.Value, StringComparison.Ordinal);
        public static bool operator ==(Phone left, Phone right) => left?.Equals(right) ?? right is null;
        public static bool operator !=(Phone left, Phone right) => !(left == right);

        public static Result<Phone> TryCreate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Result<Phone>.Failure("El teléfono no puede estar vacío");

            var digitsOnly = new string(value.Where(char.IsDigit).ToArray());

            if (digitsOnly.Length < MinLength)
                return Result<Phone>.Failure($"El teléfono debe tener al menos {MinLength} dígitos");

            if (digitsOnly.Length > MaxLength)
                return Result<Phone>.Failure($"El teléfono no puede exceder {MaxLength} dígitos");

            if (!PhoneRegex.IsMatch(value))
                return Result<Phone>.Failure("Formato de teléfono inválido");

            if (digitsOnly.Length > 0 && !char.IsDigit(digitsOnly[0]))
                return Result<Phone>.Failure("El teléfono debe comenzar con un dígito");

            try
            {
                return Result<Phone>.Success(new Phone(value));
            }
            catch (Exception ex)
            {
                return Result<Phone>.Failure($"Error inesperado al crear el teléfono: {ex.Message}");
            }
        }
    }

    [Serializable]
    public sealed class SSN : IEquatable<SSN>, IComparable<SSN>
    {
        public string Value { get; }

        private const int RequiredDigits = 9;

        private static readonly HashSet<string> InvalidSSNs = new HashSet<string>
        {
            "000000000", "111111111", /* ... otros patrones inválidos ... */ "999999999",
            "123456789", "987654321", "000112222"
        };

        private static readonly Regex[] Patterns =
        {
            new Regex(
                @"^\d{3}-\d{2}-\d{4}$",
                RegexOptions.Compiled | RegexOptions.CultureInvariant,
                TimeSpan.FromMilliseconds(250)
            ),
            new Regex(
                @"^\d{9}$",
                RegexOptions.Compiled | RegexOptions.CultureInvariant,
                TimeSpan.FromMilliseconds(250)
            )
        };

        public SSN(string ssn)
        {
            if (string.IsNullOrWhiteSpace(ssn))
                throw new InvalidSSNException("El SSN no puede estar vacío.", nameof(ssn));

            var digits = new string(ssn.Where(char.IsDigit).ToArray());
            if (digits.Length != RequiredDigits)
                throw new InvalidSSNException($"El SSN debe contener exactamente {RequiredDigits} dígitos.", nameof(ssn));

            if (!Patterns.Any(p => p.IsMatch(ssn)))
                throw new InvalidSSNException("Formato de SSN inválido. Use 'XXX-XX-XXXX' o 'XXXXXXXXX'.", nameof(ssn));

            if (InvalidSSNs.Contains(digits))
                throw new InvalidSSNException("SSN no válido (número reservado o inválido).", nameof(ssn));

            if (!IsValidStructure(digits))
                throw new InvalidSSNException("SSN no válido (estructura de área o grupo inválida).", nameof(ssn));

            Value = digits;
        }

        private static bool IsValidStructure(string digits)
        {
            int area = int.Parse(digits.Substring(0, 3));
            int group = int.Parse(digits.Substring(3, 2));

            // El código de área no puede ser 000, 666 ni >= 900
            if (area == 0 || area == 666 || area >= 900)
                return false;

            // El código de grupo no puede ser 00
            if (group == 0)
                return false;

            return true;
        }

        public string Formatted => $"{Value.Substring(0, 3)}-{Value.Substring(3, 2)}-{Value.Substring(5, 4)}";
        public string Masked => $"***-**-{Value.Substring(5, 4)}";

        public override string ToString() => Formatted;
        public override bool Equals(object obj) => Equals(obj as SSN);
        public bool Equals(SSN other) => other != null && Value == other.Value;
        public override int GetHashCode() => Value.GetHashCode(StringComparison.Ordinal);
        public int CompareTo(SSN other) => other == null ? 1 : string.Compare(Value, other.Value, StringComparison.Ordinal);

        public static bool operator ==(SSN left, SSN right) => Equals(left, right);
        public static bool operator !=(SSN left, SSN right) => !Equals(left, right);

        public static Result<SSN> TryCreate(string ssn)
        {
            if (string.IsNullOrWhiteSpace(ssn))
                return Result<SSN>.Failure("El SSN no puede estar vacío");

            var digits = new string(ssn.Where(char.IsDigit).ToArray());

            if (digits.Length != RequiredDigits)
                return Result<SSN>.Failure($"El SSN debe contener exactamente {RequiredDigits} dígitos");

            if (!Patterns.Any(p => p.IsMatch(ssn)))
                return Result<SSN>.Failure("Formato de SSN inválido. Use 'XXX-XX-XXXX' o 'XXXXXXXXX'");

            if (InvalidSSNs.Contains(digits))
                return Result<SSN>.Failure("SSN no válido (número reservado o inválido)");

            if (!IsValidStructure(digits))
                return Result<SSN>.Failure("SSN no válido (estructura de área o grupo inválida)");

            try
            {
                return Result<SSN>.Success(new SSN(ssn));
            }
            catch (Exception ex)
            {
                return Result<SSN>.Failure($"Error inesperado al crear el SSN: {ex.Message}");
            }
        }
    }

    [Serializable]
    public sealed class Address : IEquatable<Address>, IComparable<Address>
    {
        public string Value { get; }

        private const int RequiredMinLength = 5;
        private const int RequiredMaxLength = 200;

        public Address(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new InvalidAddressException("La dirección no puede estar vacía.", nameof(address));

            var trimmed = address.Trim();
            if (trimmed.Length < RequiredMinLength)
                throw new InvalidAddressException($"La dirección debe tener al menos {RequiredMinLength} caracteres.", nameof(address));

            if (trimmed.Length > RequiredMaxLength)
                throw new InvalidAddressException($"La dirección no puede exceder {RequiredMaxLength} caracteres.", nameof(address));

            Value = trimmed;
        }

        public override string ToString() => Value;
        public override bool Equals(object obj) => Equals(obj as Address);
        public bool Equals([AllowNull] Address other) => other != null && string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);
        public override int GetHashCode() => StringComparer.OrdinalIgnoreCase.GetHashCode(Value);
        public int CompareTo([AllowNull] Address other) => other == null ? 1 : string.Compare(Value, other.Value, StringComparison.OrdinalIgnoreCase);
        public static bool operator ==(Address left, Address right) => Equals(left, right);
        public static bool operator !=(Address left, Address right) => !Equals(left, right);
        public static Result<Address> TryCreate(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                return Result<Address>.Failure("La dirección no puede estar vacía");

            var trimmed = address.Trim();

            if (trimmed.Length < RequiredMinLength)
                return Result<Address>.Failure($"La dirección debe tener al menos {RequiredMinLength} caracteres");

            if (trimmed.Length > RequiredMaxLength)
                return Result<Address>.Failure($"La dirección no puede exceder {RequiredMaxLength} caracteres");

            try
            {
                return Result<Address>.Success(new Address(trimmed));
            }
            catch (Exception ex)
            {
                return Result<Address>.Failure($"Error inesperado al crear la dirección: {ex.Message}");
            }
        }
    }

    public sealed class Oficio : IEquatable<Oficio>, IComparable<Oficio>
    {
        public string Value { get; }
        private const int MaxLength = 50;
        private const int MinLength = 3;

        private static readonly Regex OficioRegex = new Regex(
            @"^[\p{L}\s\-]+$",
            RegexOptions.Compiled | RegexOptions.CultureInvariant,
            TimeSpan.FromMilliseconds(250));

        public Oficio(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El oficio no puede estar vacío", nameof(value));

            var trimmedValue = value.Trim();

            if (trimmedValue.Length < MinLength)
                throw new ArgumentException($"El oficio debe tener al menos {MinLength} caracteres", nameof(value));

            if (trimmedValue.Length > MaxLength)
                throw new ArgumentException($"El oficio no puede exceder {MaxLength} caracteres", nameof(value));

            if (!OficioRegex.IsMatch(trimmedValue))
                throw new ArgumentException("El oficio contiene caracteres inválidos", nameof(value));

            Value = trimmedValue.ToLowerInvariant();
        }

        public override string ToString() => Value;
        public override bool Equals(object obj) => obj is Oficio other && Equals(other);
        public bool Equals([AllowNull] Oficio other) => other != null && Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);
        public override int GetHashCode() => Value.GetHashCode(StringComparison.OrdinalIgnoreCase);
        public int CompareTo([AllowNull] Oficio other) => string.Compare(Value, other?.Value, StringComparison.OrdinalIgnoreCase);
        public static bool operator ==(Oficio left, Oficio right) => left?.Equals(right) ?? right is null;
        public static bool operator !=(Oficio left, Oficio right) => !(left == right);
        public bool EsMismoOficio(Oficio other) =>
            other != null && Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);

        public static Result<Oficio> TryCreate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Result<Oficio>.Failure("El oficio no puede estar vacío");

            var trimmedValue = value.Trim();

            if (trimmedValue.Length < MinLength)
                return Result<Oficio>.Failure($"El oficio debe tener al menos {MinLength} caracteres");

            if (trimmedValue.Length > MaxLength)
                return Result<Oficio>.Failure($"El oficio no puede exceder {MaxLength} caracteres");

            if (!OficioRegex.IsMatch(trimmedValue))
                return Result<Oficio>.Failure("El oficio contiene caracteres inválidos");

            return Result<Oficio>.Success(new Oficio(trimmedValue));
        }
    }

    // Result Generic
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T Value { get; }
        public string Error { get; }

        private Result(T value)
        {
            IsSuccess = true;
            Value = value;
            Error = null;
        }

        private Result(string error)
        {
            IsSuccess = false;
            Value = default;
            Error = error;
        }

        public static Result<T> Success(T value) => new Result<T>(value);
        public static Result<T> Failure(string error) => new Result<T>(error);

        public void Deconstruct(out bool isSuccess, out T value, out string error)
        {
            isSuccess = IsSuccess;
            value = Value;
            error = Error;
        }
    }
}