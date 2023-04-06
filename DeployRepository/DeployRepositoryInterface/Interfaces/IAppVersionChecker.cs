using DeployRepository.Common.Models;

namespace DeployRepositoryInterface.Interfaces
{
    public interface IAppVersionChecker
    {
        public AppVersionModel GetLastVersionNumber();
        public bool Exists(AppVersionModel version);
    }
}
