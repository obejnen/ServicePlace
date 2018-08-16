using CloudinaryDotNet.Actions;
using Newtonsoft.Json.Linq;

namespace ServicePlace.Logic.Interfaces
{
    public interface IImageService
    {
        string Upload(string filePath);
    }
}