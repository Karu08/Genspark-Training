using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace FunctionApp2;

public class GenerateSas
{
    private readonly ILogger<GenerateSas> _logger;

    public GenerateSas(ILogger<GenerateSas> logger)
    {
        _logger = logger;
    }

    [Function("GenerateSas")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "generate-sas/{blobName}")] HttpRequestData req,
        string blobName)
    {
        _logger.LogInformation($"Generating SAS for blob: {blobName}");

        string connectionString = Environment.GetEnvironmentVariable("AzureStorageConnectionString");
        string containerName = Environment.GetEnvironmentVariable("ContainerName");
        string keyVaultUri = Environment.GetEnvironmentVariable("KeyVaultUri");

        if (string.IsNullOrEmpty(connectionString) || string.IsNullOrEmpty(containerName) || string.IsNullOrEmpty(keyVaultUri))
        {
            var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
            await errorResponse.WriteStringAsync("Missing required configuration settings.");
            return errorResponse;
        }

        var blobServiceClient = new BlobServiceClient(connectionString);
        var accountName = blobServiceClient.AccountName;

        string accountKey = null;
        foreach (var part in connectionString.Split(';'))
        {
            if (part.StartsWith("AccountKey=", StringComparison.OrdinalIgnoreCase))
            {
                accountKey = part.Substring("AccountKey=".Length);
                break;
            }
        }

        if (string.IsNullOrEmpty(accountKey))
        {
            var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
            await errorResponse.WriteStringAsync("AccountKey not found in connection string.");
            return errorResponse;
        }

        var credential = new StorageSharedKeyCredential(accountName, accountKey);
        var blobClient = blobServiceClient.GetBlobContainerClient(containerName).GetBlobClient(blobName);

        DateTimeOffset expiresOn = DateTimeOffset.UtcNow.AddHours(1);
        var sasBuilder = new BlobSasBuilder
        {
            BlobContainerName = containerName,
            BlobName = blobName,
            Resource = "b",
            ExpiresOn = expiresOn
        };
        sasBuilder.SetPermissions(BlobSasPermissions.Read | BlobSasPermissions.Write);

        var sasUri = blobClient.GenerateSasUri(sasBuilder);

        var secretClient = new SecretClient(new Uri(keyVaultUri), new DefaultAzureCredential());
        string secretName = "karunya-images-sas";
        
        var secretToStore = new KeyVaultSecret(secretName, sasUri.ToString())
        {
            Properties =
            {
                Tags = { { "ExpiresOn", expiresOn.UtcDateTime.ToString("o") } }
            }
        };

        await secretClient.SetSecretAsync(secretToStore);

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(new
        {
            sasUrl = sasUri.ToString(),
            expiresOn
        });

        return response;
    }
}
