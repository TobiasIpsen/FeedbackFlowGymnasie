using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace feedbackFlowAPI.Services.Interfaces
{
    public interface IStorageService
    {

        Task<string> UploadFileAsync(IFormFile file, string bucketName);

    }
}
