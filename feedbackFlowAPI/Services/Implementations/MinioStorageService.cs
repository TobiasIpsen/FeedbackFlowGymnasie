using Minio;
using Minio.DataModel.Args;
using feedbackFlowAPI.Services.Interfaces;

namespace feedbackFlowAPI.Services.Implementations
{
    public class MinioStorageService : IStorageService
    {
        private readonly IMinioClient _minioClient;
        //test
        public MinioStorageService(IConfiguration configuration)
        {
           
            var endpoint = configuration["MinioSettings:Endpoint"];
            var accessKey = configuration["MinioSettings:AccessKey"];
            var secretKey = configuration["MinioSettings:SecretKey"];

            _minioClient = new MinioClient()
                .WithEndpoint(endpoint)
                .WithCredentials(accessKey, secretKey)
                .Build();
        }

        public async Task<string> UploadFileAsync(IFormFile file, string bucketName)
        {

            var beArgs = new BucketExistsArgs().WithBucket(bucketName);
            bool found = await _minioClient.BucketExistsAsync(beArgs);
            if (!found)
            {
                await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));

            }


            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            using var stream = file.OpenReadStream();
            await _minioClient.PutObjectAsync(new PutObjectArgs()
                .WithBucket(bucketName)
                .WithObject(fileName)
                .WithStreamData(stream)
                .WithObjectSize(file.Length)
                .WithContentType(file.ContentType));



            return $"http://localhost:9000/{bucketName}/{fileName}";
        }
    }
}