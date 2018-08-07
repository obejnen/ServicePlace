//using System;
//using ServicePlace.Model;
//using System.Collections.Generic;

//namespace ServicePlace.Logic
//{
//    public static class ExecutorInitializer
//    {
//        private static ProviderRepository _service;
//        public static ProviderRepository GetService(int count)
//        {
//            if (_service == null)
//            {
//                _service = new ProviderRepository(new List<Provider>());

//                for (int i = 1; i <= count; i++)
//                {
//                    var executor = new Provider
//                    {
//                        Id = i,
//                        Title = $"Provider title #{i}",
//                        Body = $"Provider body #{i}"
//                    };
//                    _service.AddExecutor(executor);
//                }
//            }

//            return _service;
//        }
//    }
//}