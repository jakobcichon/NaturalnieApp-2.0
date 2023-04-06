using DeployRepositoryInterface.Interfaces;

namespace DeployRepository.Interfaces.Interfaces
{
    public interface IDeployRepositoryManager: IAppVersionDownloader, IAppVersionChecker, IAppVersionPublisher
    {
    }
}
