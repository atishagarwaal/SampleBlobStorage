using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

public class BlobService
{
    private readonly string _connectionString = "DefaultEndpointsProtocol=https;AccountName=mytrialstorage123;AccountKey=P3HElT4i9MAldyKUCL5y/olxRdF7ukaXDwel7KjmSeckdmKUwveDzQk5CdZya0eyWyWdiBSY/Sm2+ASt54eSxA==;EndpointSuffix=core.windows.net";

    // Method to upload file to blob
    public async Task<string> UploadFileAsync(IFormFile file, string containerName)
    {
        // Create container if it does not exist
        BlobContainerClient containerClient = new BlobContainerClient(_connectionString, containerName);
        await containerClient.CreateIfNotExistsAsync();

        string blobName = Path.GetFileName(file.FileName);
        BlobClient blobClient = containerClient.GetBlobClient(blobName);

        // Upload or replace the blob
        using (var stream = file.OpenReadStream())
        {
            await blobClient.UploadAsync(stream, overwrite: true);
        }

        return blobClient.Uri.ToString();
    }

    // Method to download file from blob
    public async Task DownloadFileAsync(string fileUrl, string downloadPath)
    {
        BlobClient blobClient = new BlobClient(new Uri(fileUrl));
        var containerName = blobClient.BlobContainerName;

        // Create container if it does not exist
        BlobContainerClient containerClient = new BlobContainerClient(_connectionString, containerName);

        string blobName = Path.GetFileName(fileUrl);
        blobClient = new BlobClient(_connectionString, containerName, blobName);

        // Download blob to file
        using (var fileStream = File.OpenWrite(downloadPath))
        {
            await blobClient.DownloadToAsync(fileStream);
            fileStream.Close();
        }
    }
}
