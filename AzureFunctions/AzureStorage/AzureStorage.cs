using Azure.Storage.Files.Shares;
using AzureStorage.Share;

namespace AzureStorage
{
    public class AzureStorage
    {
        public required string ConnectionString { get; init; }
        public required string ShareName { get; init; }

        public bool IsInitilized  => !(_shareClient is null);
        
        private ShareClient? _shareClient;

        public void Initailize()
        {
            _shareClient = new(ConnectionString, ShareName);
        }
    
        public async Task GetAllFilesUnderDirectory(string shareDirectory, string outputPath)
        {
            CheckShare();

            ShareDirectoryClient directory = _shareClient!.GetDirectoryClient(shareDirectory);

            if (!await directory.ExistsAsync())
            {
                throw new DirectoryNotFoundException($"Directory {shareDirectory} was not found under {ShareName} share.");
            }

            var enumerator = directory.GetFilesAndDirectoriesAsync().GetAsyncEnumerator();

            try
            {
                while (await enumerator.MoveNextAsync())
                {
                    var fileClient = directory.GetFileClient(enumerator.Current.Name);
                    var download = await fileClient.DownloadAsync();

                    Directory.CreateDirectory(outputPath);

                    using (FileStream stream = File.OpenWrite(Path.Combine(outputPath, fileClient.Name)))
                    {
                        await download.Value.Content.CopyToAsync(stream);
                        await stream.FlushAsync();
                        stream.Close();

                        // Display where the file was saved
                        Console.WriteLine($"File downloaded: {stream.Name}");
                    }
                }
            }
            finally
            {
                await enumerator.DisposeAsync();
            }
        }

        public async Task AddFile(string filePath, string directoryName)
        {
            CheckShare();

            if(!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File {Path.GetFileName(filePath)} not found under {Directory.GetParent(filePath)}");
            }

            await CreateShareIfNotExist(_shareClient!, ShareName);
 
            ShareDirectoryClient directory = _shareClient!.GetDirectoryClient(directoryName);

            if(!await directory.ExistsAsync()) 
            {
                await directory.CreateAsync();
            }

            ShareFileClient file = directory.GetFileClient(Path.GetFileName(filePath));
            using (FileStream stream = File.OpenRead(filePath))
            {
                file.Create(stream.Length);
                await file.UploadAsync(stream);
            }
        }

        public async Task GetFileNamesUnder(string shareDirectory)
        {
            CheckShare();

            await CreateShareIfNotExist(_shareClient!, ShareName);

            ShareDirectoryClient directory = _shareClient.GetDirectoryClient(directoryName);

            if (!await directory.ExistsAsync())
            {
                await directory.CreateAsync();
            }

            ShareFileClient file = directory.GetFileClient(Path.GetFileName(filePath));
            using (FileStream stream = File.OpenRead(filePath))
            {
                file.Create(stream.Length);
                await file.UploadAsync(stream);
            }
        }

        public async Task CreateShareIfNotExist(ShareClient shareClient, string shareName)
        {
            var options = new Azure.Storage.Files.Shares.Models.ShareCreateOptions()
            {
                Protocols = Azure.Storage.Files.Shares.Models.ShareProtocols.Smb
            };

            await shareClient.CreateIfNotExistsAsync(options);

            if (!await shareClient.ExistsAsync())
            {
                throw new FileShareNotExistException($"Azure File Share named {ShareName} was not found in Azure.");
            }
        }

        private void CheckShare()
        {
            if (!IsInitilized)
            {
                throw new FileShareNotInitializedException();
            }
        }
    }
}