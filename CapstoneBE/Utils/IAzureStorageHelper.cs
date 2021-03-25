using Azure.Storage.Blobs;

namespace CapstoneBE.Utils
{
    public interface IAzureStorageHelper
    {
        BlobClient GetBlobClient(string containerName, string blobName);
        BlobContainerClient GetContainerClient(string containerName);
        bool DeleteBlob(BlobClient blobClient);
    }
}