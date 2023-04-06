using System.ComponentModel.DataAnnotations;

namespace DeployRepository.API.ConfigurationFile
{
    internal class ConfigurationFile : IConfigurationFile
    {
        [Required]
        public string ConnectionString { get; set; } = string.Empty;
    }
}
