using DeployRepository.Common.Models;

namespace DeployRepository.Common.Exceptions
{
    /// <summary>
    /// Exception thrwon when app version was already published to the server.
    /// </summary>
    public sealed class AppVersionExistException: Exception
    {
        public AppVersionExistException(AppVersionModel model) : 
            base($"Given app version ({model}) was already published to the server.")
        {
            
        }
    }
}
