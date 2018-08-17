using ServicePlace.Logic.Interfaces.Services;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace ServicePlace.Logic.Services
{
    public class ImageService : IImageService
    {
        private readonly Cloudinary _cloudinary;
        public ImageService()
        {
            var account = new Account("railsimagecloud", "984753989472299", "x3Qi_omT-_dYS4ydUce7zfu2Qw0");
            _cloudinary = new Cloudinary(account);
        }
        public string Upload(string filePath)
        {
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(filePath)
            };
            return _cloudinary.Upload(uploadParams).Uri.ToString();
        }
    }
}