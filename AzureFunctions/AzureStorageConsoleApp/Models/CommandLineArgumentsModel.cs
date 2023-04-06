using CommandLine;

namespace AzureStorageConsoleApp.Models
{
    internal class CommandLineArgumentsModel
    {
        [Option('c' , "connectionString", Required = true, HelpText = "Connection string to azure file share.")]
        public bool ConnectionString { get; set; }

        [Option('s', "shareName", Required = true, HelpText = "File share name.")]
        public bool ShareName { get; set; }

        [Option('d', "deployPath", Required = true, HelpText = "Path under which deploy files will be placed.")]
        public bool DeployPath { get; set; }

        [Option('v', "version", Required = true, HelpText = "Application version.")]
        public bool Version { get; set; }
    }
}
