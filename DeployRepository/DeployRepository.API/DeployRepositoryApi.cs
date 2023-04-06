using DeployRepository.API.ConfigurationFile;
using DeployRepositoryInterface.Interfaces;

namespace DeployRepository.API
{
    public class DeployRepositoryApi : IDeployRepositoryController
    {
        private IConfigurationFile _configurationFileModel;

        public DeployRepositoryApi(string configurationFile)
        {
            using (var reader = ConfigurationFileMapper.GetFileStream(configurationFile))
            {
                ConfigurationFileMapper.Map<ConfigurationFile.ConfigurationFile>(reader, () =>
                {

                });
            }
        }

        public IEnumerable<IRepositoryElementModel> GetAllVersionsNames()
        {
            throw new NotImplementedException();
        }

        public void GetNewestApp(string outputPath)
        {
            throw new NotImplementedException();
        }

        public void GetNewestAppVerions()
        {
            throw new NotImplementedException();
        }
    }
}
