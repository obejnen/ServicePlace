//using ServicePlace.DataProvider.DbContexts;
//using ServicePlace.DataProvider.Interfaces;
//using ServicePlace.DataProvider.Mappers;
//using ServicePlace.Model.LogicModels;

//namespace ServicePlace.DataProvider.Repositories
//{
//    public class ImageRepository : IImageRepository
//    {
//        private readonly ApplicationContext _context;
//        private readonly ImageMapper _mapper;

//        public ImageRepository(ApplicationContext context, ImageMapper mapper)
//        {
//            _context = context;
//            _mapper = mapper;
//        }

//        public void Create(Image model)
//        {
//            var image = _mapper.MapToDataModel(model);
//            _context.Images.Add(image);
//            _context.SaveChanges();
//        }
//    }
//}