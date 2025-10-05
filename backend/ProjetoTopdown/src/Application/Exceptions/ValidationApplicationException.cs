using FluentValidation.Results;
using System.Globalization;
using System.Text;

namespace ProjetoTopdown.Application.Exceptions;

public class ValidationApplicationException : Exception
{
    private static readonly CompositeFormat _message =
        CompositeFormat.Parse("Validation Errors for type {0}: {1}");

    private static readonly CompositeFormat _doesNotExistMessage =
        CompositeFormat.Parse("{0} '{1}' does not exist");

    public ValidationApplicationException()
    {
    }

    public ValidationApplicationException(string message)
        : base(message)
    {
    }

    public ValidationApplicationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public ValidationApplicationException(string typeName, ICollection<ValidationFailure> failures)
        : base(string.Format(
            CultureInfo.InvariantCulture,
            _message,
            typeName,
            string.Join(' ', failures)))
    {
    }

    public static void ThrowDoesNotExistException(string propertyName, object value)
    {
        throw new ValidationApplicationException(string.Format(
            CultureInfo.InvariantCulture,
            _doesNotExistMessage,
            propertyName,
            value));
    }
}
