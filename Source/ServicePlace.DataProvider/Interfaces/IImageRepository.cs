using ServicePlace.Model.DataModels;

namespace ServicePlace.DataProvider.Interfaces
{
    public interface IImageRepository
    {
        void Create(Image model);
    }
}