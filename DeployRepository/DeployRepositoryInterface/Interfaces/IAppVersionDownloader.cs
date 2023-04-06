using DeployRepository.Common.Models;

namespace DeployRepository.Interfaces.Interfaces
{
    public interface IAppVersionDownloader
    {
        public void GetLastVersion(string outputPath);
        public bool GetVersion(AppVersionModel versionNumber, string outputPath);
    }
}
