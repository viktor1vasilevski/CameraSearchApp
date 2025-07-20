namespace Infrastructure.Exceptions;

public class UnsupportedEntityException : Exception
{
    public UnsupportedEntityException(Type entityType)
        : base($"Entity type '{entityType.Name}' is not supported.")
    {
    }
}
