using System.Net.Http.Json;
using Azure.Storage.Blobs;

namespace BlobAPI.Services
{
    public class BlobStorageService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public BlobStorageService(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient();
        }

        private async Task<BlobClient> GetBlobClientForFile(string blobName)
        {
            var functionBaseUrl = _configuration["AzureBlob:GenerateSasFunctionUrl"];
            var functionKey = _configuration["AzureBlob:FunctionKey"];

            var fullUrl = $"{functionBaseUrl}/{blobName}?code={functionKey}";

            var response = await _httpClient.GetAsync(fullUrl);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Failed to get SAS URL. Status code: {response.StatusCode}");

            var sasResponse = await response.Content.ReadFromJsonAsync<SasResponse>();
            return new BlobClient(new Uri(sasResponse.SasUrl));
        }

        public async Task UploadFile(Stream fileStream, string fileName)
        {
            var blobClient = await GetBlobClientForFile(fileName);
            await blobClient.UploadAsync(fileStream, overwrite: true);
        }

        public async Task<Stream> DownloadFile(string fileName)
        {
            var blobClient = await GetBlobClientForFile(fileName);
            if (await blobClient.ExistsAsync())
            {
                var downloadInfo = await blobClient.DownloadStreamingAsync();
                return downloadInfo.Value.Content;
            }
            return null;
        }

        private class SasResponse
        {
            public string SasUrl { get; set; }
            public DateTime ExpiresOn { get; set; }
        }
    }
}
