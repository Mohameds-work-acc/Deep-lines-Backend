using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Deep_lines_Backend.Domain.Models.SharedDTOs.CloudinaryDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace Deep_lines_Backend.Extensions
{
    public class CloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IConfiguration config)
        {
            var account = new Account(
                config["Cloudinary:CloudName"],
                config["Cloudinary:ApiKey"],
                config["Cloudinary:ApiSecret"]
            );

            _cloudinary = new Cloudinary(account);
        }

        public async Task<CloudinaryUploadResponse> UploadImageAsync(IFormFile file, string folderName)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is null or empty.");
            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName + Guid.NewGuid(), stream),
                Folder = "deepLines/" + folderName,
                Overwrite = false,
                UniqueFilename = true
            };
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            if (uploadResult.StatusCode != HttpStatusCode.OK)
                throw new Exception("Image upload failed: " + uploadResult.Error?.Message);
            return new CloudinaryUploadResponse
            {
                Url = uploadResult.SecureUrl.ToString(),
                PublicId = uploadResult.PublicId
            };
        }
        public async Task<bool> DeleteImageAsync(string publicId)
        {
            var deletionParams = new DeletionParams(publicId);
            var deletionResult = await _cloudinary.DestroyAsync(deletionParams);
            return deletionResult.Result == "ok";

        }
        public async Task<CloudinaryUploadResponse> UpdateImageAsync(string publicId, IFormFile newFile, string folderName)
        {
            var deleteResult = await DeleteImageAsync(publicId);
            if (!deleteResult)
                throw new Exception("Failed to delete existing image.");
            return await UploadImageAsync(newFile, folderName);
        }
    }
}
