namespace learning_center_webapi.Contexts.Security.Domain.Model.Exceptions;

public class MissingJwtSecretException : Exception
{
    public MissingJwtSecretException()
        : base("La clave secreta de autenticación no está configurada.")
    {
    }

    public MissingJwtSecretException(string? message)
        : base(message)
    {
    }

    public MissingJwtSecretException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}
