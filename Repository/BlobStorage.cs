using System;
using System.IO;
using Azure.Storage;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using Microsoft.Azure.Devices;

namespace StorageCRUD.Repository
{
    public class BlobStorage
    {
        private static string connectionString = "DefaultEndpointsProtocol=https;AccountName=shitalstoragedemo;AccountKey=ai2FQ7MTHBaHt2RTcW+DxA4ILuXFmdpz8lh8LGW539OkVv/eWqxK6b+JLedwDdDe//jRH1MTzAWq+AStuoLo5Q==;EndpointSuffix=core.windows.net";
        public static async Task CreateBlob(string blobName)
        {       
            if (string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentNullException("Enter Blob Name");
            }
            try
            {
                BlobContainerClient container = new BlobContainerClient(connectionString, blobName);
                await container.CreateAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static async Task DeleteBlob(string blobName)
        {
            if (string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentNullException("Enter Blob Name");
            }
            try
            {
                BlobContainerClient container = new BlobContainerClient(connectionString, blobName);
                await container.DeleteAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static async Task DeleteBlobContent(string blobName, string file)
        {
            if (string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentNullException("Enter Blob Name");
            }
            try
            {
                BlobContainerClient container = new BlobContainerClient(connectionString, blobName);
                await container.DeleteBlobAsync(file);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static async Task<BlobProperties> UpdateBlobContent(string blobName,string file)
        {
            if (string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentNullException("Enter Blob Name");
            }
            try
            {
                string fileName = Path.GetTempFileName();
                BlobContainerClient container = new BlobContainerClient(connectionString,blobName);
                BlobClient blob = container.GetBlobClient(file);
                await blob.UploadAsync(fileName);
                BlobProperties prop = await blob.GetPropertiesAsync();
                return prop;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static async Task<BlobProperties> GetBlobContent(string blobName, string file)
        {
            if (string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentNullException("Enter Blob Name");
            }
            try
            {
                BlobContainerClient container = new BlobContainerClient(connectionString, blobName);
                BlobClient blob = container.GetBlobClient(file);
                BlobProperties prop = await blob.GetPropertiesAsync();
                return prop;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static async Task<List<string>> GetBlob(string blobName, string file)
        {
            if (string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentNullException("Enter Blob Name");
            }
            try
            {
                BlobContainerClient container = new BlobContainerClient(connectionString, blobName);
                List<string> names = new List<string>();
                await foreach(BlobItem a in container.GetBlobsAsync())
                {
                    names.Add(a.Name);
                }
                return names;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static async Task<BlobProperties> DownloadBlob(string blobName, string file)
        {
            try
            {
                string path = @"C:\Users\vmadmin\Desktop\Shital_AzureIOT\StorageCRUD\Downloads\"+file;
                BlobContainerClient container = new BlobContainerClient(connectionString, blobName);
                BlobClient client = container.GetBlobClient(file);
                await client.DownloadToAsync(path);
                BlobProperties prop = await client.GetPropertiesAsync();
                return prop;

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
