using Azure.Storage.Blobs;
using System;

namespace CapstoneBE.Utils
{
    public class AzureStorageHelper : IAzureStorageHelper
    {
        public BlobClient GetBlobClient(string containerName, string blobName)
        {
            BlobContainerClient blobContainerClient = GetContainerClient(containerName);
            if (blobContainerClient.Exists())
            {
                BlobClient blobClient = blobContainerClient.GetBlobClient(blobName);
                return blobClient;
            }
            return null;
        }

        public bool DeleteBlob(BlobClient blobClient)
        {
            return blobClient.DeleteIfExists();
        }

        public BlobContainerClient GetContainerClient(string containerName)
        {
            string connectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");
            // Create a BlobServiceClient object which will be used to create a container client
            BlobServiceClient blobServiceClient = new(connectionString);
            BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
            return blobContainerClient;
        }
    }
}