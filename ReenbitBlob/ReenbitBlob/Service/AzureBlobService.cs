using Azure.Storage;
//using Azure.Identity;
//using Azure.Security.KeyVault.Secrets;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using ReenbitBlob.Interface;
using ReenbitBlob.Models;

namespace ReenbitBlob.Configuration
{
    public class AzureBlobService : IAzureBlobService
    {
        public readonly string storageAccount = "reenbitblobtesttask";
        public readonly string accessKey = "lK0cYjXVBknXLgp7iQk17+vnEOqD09Ox8G5swAsoqodinJzkrFVHD6f8T8bL6J2qads5XtQipsY4+ASt3kwPRg==";
        private readonly BlobServiceClient blobServiceClient;
        public AzureBlobService()
        {
            /*string keyVaultUrl = "https://AzureStorageAccessKey.vault.azure.net/";
            string secretName = "AccessKey";
            var client = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());
            KeyVaultSecret secret = client.GetSecret(secretName);
            accessKey = secret.Value;*/

            var credentials = new StorageSharedKeyCredential(storageAccount, accessKey);
            var blobUri = $"https://{storageAccount}.blob.core.windows.net";
            blobServiceClient = new BlobServiceClient(new Uri(blobUri), credentials);
        }
        public async Task UploadAsync(EmailPackageModel model)
        {
            string containerName = "blobstorage";
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            var blobClient = containerClient.GetBlobClient(model.File.FileName);
            var metadata = new Dictionary<string, string>()
            { 
                { "Email", model.Email },
            };
            using (var stream = model.File.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = model.File.ContentType }, metadata);
            }
        }
    }
}
