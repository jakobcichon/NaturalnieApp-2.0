using DeployRepository.Common.Models;

namespace DeployRepository.Interfaces.Interfaces
{
    public interface IAppVersionPublisher
    {
        public bool PublishVersion(AppVersionModel appVersion, string filesPath);
    }
}
