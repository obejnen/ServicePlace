//using System;
//using System.Collections.Generic;
//using System.Linq;
//using ServicePlace.Model;

//namespace ServicePlace.Logic
//{
//    public class ProviderRepository
//    {
//        public ProviderRepository(List<Provider> executors)
//        {
//            Providers = executors;
//        }

//        public ProviderRepository()
//        {
//            Providers = new List<Provider>();
//        }

//        public List<Provider> Providers { get; }
//        public Provider GetExecutors(int id) => Providers.FirstOrDefault(x => x.Id == id);

//        public void AddExecutor(Provider executor)
//        {
//            Providers.Add(executor);
//        }

//        public void RemoveExecutor(int id) => Providers.Remove(Providers.FirstOrDefault(x => x.Id == id));
//    }
//}
