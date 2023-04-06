using DeployRepositoryInterface.Interfaces;

namespace DeployRepository.Interfaces.Interfaces.Infrastructure
{
    public interface IAppVersionPersistanceStore : IAppVersionDownloader, IAppVersionChecker, IAppVersionPublisher
    {
        public bool IsConnected { get; }
    }
}
