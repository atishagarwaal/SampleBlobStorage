using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SampleBlobStorage;

class Program
{
    static async Task Main(string[] args)
    {
        // Create an instance of BlobService
        BlobService blobService = new BlobService();

        // Create a sample IFormFile from a local file
        string currentDirectory = Directory.GetCurrentDirectory();
        string filePath = Path.Combine(currentDirectory, "..\\..\\..\\", "File1.txt");
        IFormFile sampleFile = FileUtility.CreateSampleFormFile(filePath);

        // Upload the sample IFormFile
        Console.WriteLine("Uploading file...");
        string fileUrl = await blobService.UploadFileAsync(sampleFile, "container1");
        Console.WriteLine($"File uploaded to: {fileUrl}");

        // Download the file
        Console.WriteLine("Downloading file...");
        string downloadfilename = "File2.txt";
        string downloadPath = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\", downloadfilename);
        await blobService.DownloadFileAsync(fileUrl, downloadPath);
        Console.WriteLine($"File downloaded to: {downloadPath}");

    }
}