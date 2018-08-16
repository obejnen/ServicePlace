using ServicePlace.Model.LogicModels;

namespace ServicePlace.DataProvider.Interfaces
{
    public interface IImageRepository
    {
        void Create(Image model);
    }
}