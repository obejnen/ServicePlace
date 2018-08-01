//using AutoMapper;
//using ServicePlace.DataProvider.Interfaces;

//namespace ServicePlace.DataProvider.EntityConverters
//{
//    public class StringTypeConverter<T> : ITypeConverter<string, T> where T : class
//    {
//        private readonly IRepository<T> _repository;

//        public StringTypeConverter(IRepository<T> repository)
//        {
//            _repository = repository;
//        }

//        public T Convert(string id, T destination, ResolutionContext context)
//        {
//            return _repository.FindByIdAsync(id).Result;
//        }
//    }
//}