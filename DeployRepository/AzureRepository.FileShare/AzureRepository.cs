using DeployRepository.Common.Models;
using DeployRepository.Interfaces.Interfaces.Infrastructure;

namespace AzureRepository.FileShare
{
    public class AzureRepository : IAppVersionPersistanceStore
    {
        public string ConnectionString { get; init; } = string.Empty;
        public string ShareName { get; init; } = string.Empty;

        private AzureStorage.AzureStorage _store;

        public bool IsConnected { get => throw new NotImplementedException(); }

        public bool Exists(AppVersionModel version)
        {
            throw new NotImplementedException();
        }

        public void GetLastVersion(string outputPath)
        {
            throw new NotImplementedException();
        }

        public AppVersionModel GetLastVersionNumber()
        {
            throw new NotImplementedException();
        }

        public bool GetVersion(AppVersionModel versionNumber, string outputPath)
        {
            throw new NotImplementedException();
        }

        public bool PublishVersion(AppVersionModel appVersion, string filesPath)
        {
            throw new NotImplementedException();
        }
    }
}