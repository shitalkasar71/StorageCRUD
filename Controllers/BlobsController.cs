using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using StorageCRUD.Repository;
namespace StorageCRUD.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    [Route("[controller]")]
    public class BlobsController : ControllerBase
    {
        [HttpPost("AddBlob")]
        public async Task<string> AddBlob(string blobName)
        {
            await Repository.BlobStorage.CreateBlob(blobName);
            return null;
        }
       
        [HttpDelete("DeleteBlobContent")]
        public async Task<string> DeleteBlobContent(string blobName, string file)
        {
            await Repository.BlobStorage.DeleteBlobContent(blobName, file);
            return null;
        }
        [HttpDelete("DeleteBlob")]
        public async Task<string> DeleteBlob(string blobName)
        {
            await Repository.BlobStorage.DeleteBlob(blobName);
            return null;
        }
        [HttpPut("UpdateBlobContent")]
        public async Task<string> UpdateBlobContent(string blobName, string file)
        {
            await Repository.BlobStorage.UpdateBlobContent(blobName, file);
            return null;
        }
        [HttpGet("GetBlobContent")]
        public async Task<BlobProperties> GetBlobContent(string blobName, string file)
        {
            var data=await Repository.BlobStorage.GetBlobContent(blobName, file);
            return data;
        }
        [HttpGet("GetBlob")]
        public async Task<List<string>> GetBlob(string blobName, string file)
        {
            var data = await Repository.BlobStorage.GetBlob(blobName, file);
            return data;
        }
        [HttpGet("DownloadBlobContent")]
        public async Task<BlobProperties> DownloadBlobContent(string blobName, string file)
        {
            var data = await Repository.BlobStorage.DownloadBlob(blobName, file);
            return data;
        }
    }
}
