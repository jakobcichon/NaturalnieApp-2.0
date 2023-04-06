using System.Runtime.CompilerServices;
using static System.Net.WebRequestMethods;

namespace AzureStorage.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task AddFile()
        {
            var testFilePath = Path.Combine(Environment.CurrentDirectory, "Resources", "v1.0.61", "NaturalnieAppInstaller.msi");
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=naturalniedeploystorage;AccountKey=jyERbXyCVbJMvihzX9EzsdzT0RFbNOm/+QOZSYh8puz4AjLhpL07UEHroP2tpie3+sstglCi9IHT+AStLMOArA==;EndpointSuffix=core.windows.net";
            var shareName = "naturalnieappdeploy";

            AzureStorage storage = new AzureStorage() { 
                ConnectionString = connectionString, 
                ShareName = shareName
            };
            await storage.AddFile(testFilePath, @"NaturalnieApp2DeployVersions\v1.0.0.0");
        }

        [Test]
        public async Task GetFiles()
        {
            var testFilePath = Path.Combine(Environment.CurrentDirectory, "Resources", "Downloaded");
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=naturalniedeploystorage;AccountKey=jyERbXyCVbJMvihzX9EzsdzT0RFbNOm/+QOZSYh8puz4AjLhpL07UEHroP2tpie3+sstglCi9IHT+AStLMOArA==;EndpointSuffix=core.windows.net";
            var shareName = "naturalnieappdeploy";

            AzureStorage storage = new AzureStorage()
            {
                ConnectionString = connectionString,
                ShareName = shareName
            };
            await storage.GetAllFilesUnderDirectory(@"NaturalnieApp2DeployVersions\v1.0.0.0", testFilePath);
        }
    }
}