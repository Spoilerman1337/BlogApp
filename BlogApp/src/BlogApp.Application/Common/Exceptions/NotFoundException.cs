namespace BlogApp.Application.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key}) not found.") { }
    public NotFoundException(string name, object key1, object key2) : base($"Entity \"{name}\" ({key1};{key2}) not found.") { }
}
