namespace AzureStorage.Share
{
    public class FileShareNotExistException: Exception
    {
        public FileShareNotExistException(): base() { }
        public FileShareNotExistException(string? message) : base(message) { }
    }
}
