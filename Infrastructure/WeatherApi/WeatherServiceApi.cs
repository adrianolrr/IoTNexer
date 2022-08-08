using Azure.Storage;
using Azure.Storage.Blobs;
using Infrastructure.Base;
using Infrastructure.Extensions;
using Infrastructure.Settings;
using Infrastructure.WeatherApi.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System.IO.Compression;
using System.Text;
using System.Text.RegularExpressions;

namespace Infrastructure.WeatherApi
{
    public class WeatherServiceApi : AzureIoTBase, IWeatherServiceApi
    {
        public WeatherServiceApi(ILogger<WeatherServiceApi> logger,
            IOptions<AzureIoTSettings> azureIoTSettings) : base(logger, azureIoTSettings)
        {
        }
        public async Task<string> ReadFileAsync(string filename,
                                    string containerName)
        {
            _logger.LogInformation($"Init loading files...file: {filename}, connection: {_azureIoTSettings.Connection}, container: {containerName}");
            try
            {
                StorageCredentials storageCredentials = new StorageCredentials(_azureIoTSettings.SasToken);
                var storageAccount = new CloudStorageAccount(storageCredentials, _azureIoTSettings.Name, null, true);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference($"{containerName}");
                CloudBlockBlob blockBlobReference = container.GetBlockBlobReference(filename);
                string allContent = string.Empty;
                using (MemoryStream blobMemStream = new())
                {
                    await blockBlobReference.DownloadToStreamAsync(blobMemStream);
                    if (CheckIfISCompressed.ValidateExtensionFile(filename))
                    {
                        using ZipArchive archive = new ZipArchive(blobMemStream);
                        foreach (ZipArchiveEntry entry in archive.Entries)
                        {
                            using (var fileStream = entry.Open())
                            {
                                using var reader = new StreamReader(fileStream, Encoding.UTF8);
                                allContent = reader.ReadToEnd();
                            }
                        }
                    }
                    else
                        allContent = Encoding.UTF8.GetString(blobMemStream.ToArray());
                }
                return allContent;
            }
            catch (StorageException e)
            {
                string path = $"{_azureIoTSettings.Blob}{containerName}/{filename}";
                _logger.LogCritical(e.Message);
                throw new FileNotFoundException($"{e.Message} Url {path}");

            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                _logger.LogInformation("Ended loading files...");
            }
        }
    }
}
