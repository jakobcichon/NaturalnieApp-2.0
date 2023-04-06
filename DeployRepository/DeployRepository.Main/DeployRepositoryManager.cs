using DeployRepository.Common.Exceptions;
using DeployRepository.Common.Models;
using DeployRepository.Interfaces.Interfaces;
using DeployRepository.Interfaces.Interfaces.Infrastructure;

namespace DeployRepository.Main
{
    public class DeployRepositoryManager : IDeployRepositoryManager
    {
        readonly IAppVersionPersistanceStore _store;

        public DeployRepositoryManager(IAppVersionPersistanceStore store)
        {
            _store = store;
        }

        public bool Exists(AppVersionModel version)
        {
            return _store.Exists(version);
        }

        public void GetLastVersion(string outputPath)
        {
            _store.GetLastVersion(outputPath);
        }

        public AppVersionModel GetLastVersionNumber()
        {
            return _store.GetLastVersionNumber();
        }

        public bool GetVersion(AppVersionModel versionNumber, string outputPath)
        {
            return _store.GetVersion(versionNumber, outputPath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appVersion"></param>
        /// <param name="filesPath"></param>
        /// <returns></returns>
        /// <exception cref="AppVersionExistException"></exception>
        public bool PublishVersion(AppVersionModel appVersion, string filesPath)
        {
            CheckIfVersionExistExist(appVersion);

            CheckIfDirectoryExist(filesPath);

            return _store.PublishVersion(appVersion, filesPath);
        }

        private static void CheckIfDirectoryExist(string filesPath)
        {
            if (!Directory.Exists(filesPath))
            {
                throw new DirectoryNotFoundException(filesPath);
            }
        }

        private void CheckIfVersionExistExist(AppVersionModel appVersion)
        {
            if (Exists(appVersion))
            {
                throw new AppVersionExistException(appVersion);
            }
        }
    }
}
