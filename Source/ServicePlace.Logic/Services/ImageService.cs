using ServicePlace.Logic.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Newtonsoft.Json.Linq;

namespace ServicePlace.Logic.Services
{
    public class ImageService : IImageService
    {
        private readonly Account account;
        private readonly Cloudinary cloudinary;
        public ImageService()
        {
            account = new Account("railsimagecloud", "984753989472299", "x3Qi_omT-_dYS4ydUce7zfu2Qw0");
            cloudinary = new Cloudinary(account);
        }
        public string Upload(string filePath)
        {
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(filePath)
            };
            return cloudinary.Upload(uploadParams).Uri.ToString();
        }
    }
}