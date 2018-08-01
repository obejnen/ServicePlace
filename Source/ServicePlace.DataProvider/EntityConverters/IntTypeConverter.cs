//using AutoMapper;
//using ServicePlace.DataProvider.Interfaces;

//namespace ServicePlace.DataProvider.EntityConverters
//{
//    public class IntTypeConverter<T> : ITypeConverter<int, T> where T : class
//    {
//        private readonly IRepository<T> _repository;

//        public IntTypeConverter(IRepository<T> repository)
//        {
//            _repository = repository;
//        }

//        public T Convert(int id, T destination, ResolutionContext context)
//        {
//            return _repository.FindByIdAsync(id).Result;
//        }
//    }
//}