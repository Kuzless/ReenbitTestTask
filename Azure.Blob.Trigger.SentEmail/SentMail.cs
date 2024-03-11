using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Collections.Generic;
using Org.BouncyCastle.Crypto.Macs;

namespace Azure.Blob.Trigger.SentEmail
{
    public class SentMail
    {
        [FunctionName("SentEmail")]
        public void Run([BlobTrigger("blobstorage/{name}", Connection = "AzureWebJobsStorage")]Stream myBlob, string name, ILogger log, IDictionary<string, string> metadata)
        {
            var sasToken = GenerateToken(Environment.GetEnvironmentVariable("AzureWebJobsStorage"), "blobstorage", name);
            var fileUriWithSas = $"https://reenbitblobtesttask.blob.core.windows.net/blobstorage/{name}{sasToken}";

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Temp", "mytempmail88467234@gmail.com"));
            message.To.Add(new MailboxAddress("", metadata["Email"]));
            message.Subject = "Your file was successfully uploaded!";
            var builder = new BodyBuilder();
            builder.TextBody = $"Your file was successfully uploaded! \n Available at: {fileUriWithSas}";
            message.Body = builder.ToMessageBody();
            SendMessage(new SmtpClient(), message);

            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
        }
        public async void SendMessage(SmtpClient client, MimeMessage message)
        {
            await client.ConnectAsync("smtp.gmail.com", 587, false);
            await client.AuthenticateAsync("mytempmail88467234@gmail.com", "afsn beja ycyb bpaw");
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
        private static string GenerateToken(string storageConnectionString, string containerName, string blobName)
        {
            var storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(containerName);
            var blob = container.GetBlockBlobReference(blobName);

            var sasConstraints = new SharedAccessBlobPolicy
            {
                SharedAccessStartTime = DateTimeOffset.UtcNow,
                SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddHours(1),
                Permissions = SharedAccessBlobPermissions.Read | SharedAccessBlobPermissions.List
            };

            return blob.GetSharedAccessSignature(sasConstraints);
        }
    }
}
