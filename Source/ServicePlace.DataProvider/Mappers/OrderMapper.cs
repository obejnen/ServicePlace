﻿//using CommonModels = ServicePlace.Model;
//using DataModels = ServicePlace.DataProvider.Models;

//namespace ServicePlace.DataProvider.Mappers
//{
//    public class OrderMapper
//    {
//        public DataModels.Order MapToDataModel(CommonModels.Order model)
//        {
//            var creator = new UserMapper().MapToDataModel(model.Creator);
//            return new DataModels.Order
//            {
//                Id = model.Id,
//                Body = model.Body,
//                Title = model.Title,
//                CreatedAt = model.CreatedAt,
//                UpdatedAt = model.UpdatedAt,
//                Creator = creator
//                //CreatorId = creator.Id
//            };
//        }

//        public CommonModels.Order MapToCommonModel(DataModels.Order model)
//        {
//            var creator = new UserMapper().MapToCommonModel(model.Creator);
//            return new CommonModels.Order
//            {
//                Id = model.Id,
//                Title = model.Title,
//                Body = model.Body,
//                Creator = creator,
//                CreatedAt = model.CreatedAt,
//                UpdatedAt = model.UpdatedAt
//            };
//        }
//    }
//}