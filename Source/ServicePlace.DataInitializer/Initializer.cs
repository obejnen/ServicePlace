using System;
using System.Data;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using FizzWare.NBuilder;
using ServicePlace.Common;
using ServicePlace.DataProvider.DbContexts;
using ServicePlace.DataProvider.ContextProviders;
using ServicePlace.DataProvider.Repositories;
using ServicePlace.Model.DataModels;
using ServicePlace.DataProvider.Managers;
using ServicePlace.DataProvider.Stores;
using ServicePlace.Logic.Services;

namespace ServicePlace.DataInitializer
{
    public class Initializer
    {
        private readonly int _dataCount;

        public CommitProvider CommitProvider { get; private set; }
        public ProviderRepository ProviderRepository { get; private set; }
        public OrderRepository OrderRepository { get; private set; }
        public IdentityRepository IdentityRepository { get; private set; }
        public OrderResponseRepository OrderResponseRepository { get; private set; }
        public ProviderResponseRepository ProviderResponseRepository { get; private set; }
        public OrderCategoryRepository OrderCategoryRepository { get; private set; }
        public ProviderCategoryRepository ProviderCategoryRepository { get; private set; }
        public ProfileRepository ProfileRepository { get; private set; }
        public ImageRepository ImageRepository { get; private set; }

        public OrderService OrderService { get; private set; }
        public ProviderService ProviderService { get; private set; }
        public UserService UserService { get; private set; }
        public ImageService ImageService { get; private set; }
        public ApplicationContext Context { get; private set; }

        public Initializer(int dataCount)
        {
            _dataCount = dataCount;
            Context = new ApplicationContext();
        }

        public void ClearDb()
        {
            var connection = new SqlConnection(@"Data Source=(localdb)\ProjectsV13;" +
                                               "Initial Catalog=ServicePlaceDb;" +
                                               "Integrated Security=True;Connect Timeout=60;" +
                                               "Encrypt=False;TrustServerCertificate=True;" +
                                               "ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            var cmd = new SqlCommand("DeleteAll", connection) {CommandType = CommandType.StoredProcedure};
            connection.Open();
            cmd.ExecuteNonQuery();
            Context.Dispose();
            connection.Close();
        }

        private void InitializeObjects()
        {
            ClearDb();
            Context = new ApplicationContext();
            CommitProvider = new CommitProvider(Context);
            IdentityRepository = new IdentityRepository(
                new UserManager(new UserStore(Context)),
                new RoleManager(new RoleStore(Context)),
                Context
            );
            OrderRepository = new OrderRepository(Context);
            OrderResponseRepository = new OrderResponseRepository(Context);
            OrderCategoryRepository = new OrderCategoryRepository(Context);
            ProviderRepository = new ProviderRepository(Context);
            ProviderResponseRepository = new ProviderResponseRepository(Context);
            ProviderCategoryRepository = new ProviderCategoryRepository(Context);
            ImageRepository = new ImageRepository(Context);
            ProfileRepository = new ProfileRepository(Context);

            OrderService = new OrderService(OrderRepository, OrderResponseRepository, OrderCategoryRepository, CommitProvider);
            ProviderService = new ProviderService(ProviderRepository, ProviderResponseRepository, ProviderCategoryRepository, CommitProvider);
            ImageService = new ImageService();
            UserService = new UserService(IdentityRepository, ProfileRepository, CommitProvider);
        }

        public void InitializeDb()
        {
            InitializeObjects();
            
            IdentityRepository.CreateRole(new Role()
            {
                Name = Constants.UserRoleName
            });

            IdentityRepository.CreateRole(new Role()
            {
                Name = Constants.AdminRoleName
            });

            var users = Builder<User>
                .CreateListOfSize(_dataCount)
                .All()
                .Build();

            foreach (var user in users)
            {
                IdentityRepository.Create(user);
            }
            Context.SaveChanges();

            users = IdentityRepository.GetAll().ToList();


            var orderCategories = Builder<OrderCategory>
                .CreateListOfSize(_dataCount)
                .Build();

            var providerCategories = Builder<ProviderCategory>
                .CreateListOfSize(_dataCount)
                .Build();

            var orders = Builder<Order>
                .CreateListOfSize(_dataCount)
                .All()
                .With(o => o.Creator = Pick<User>.RandomItemFrom(users))
                .With(o => o.Category = Pick<OrderCategory>.RandomItemFrom(orderCategories))
                .With(o => o.Closed = false)
                .With(o => o.Approved = true)
                .Build();

            var providers = Builder<Provider>
                .CreateListOfSize(_dataCount)
                .All()
                .With(p => p.Creator = Pick<User>.RandomItemFrom(users))
                .With(p => p.Category = Pick<ProviderCategory>.RandomItemFrom(providerCategories))
                .With(p => p.Approved = true)
                .Build();

            var orderResponses = Builder<OrderResponse>
                .CreateListOfSize(_dataCount)
                .All()
                .With(or => or.Provider = Pick<Provider>.RandomItemFrom(providers))
                .With(or => or.Order = Pick<Order>.RandomItemFrom(orders))
                .With(or => or.Creator = or.Provider.Creator)
                .With(or => or.Completed = false)
                .Build();

            var providerResponses = Builder<ProviderResponse>
                .CreateListOfSize(_dataCount)
                .All()
                .With(pr => pr.Order = Pick<Order>.RandomItemFrom(orders))
                .With(pr => pr.Provider = Pick<Provider>.RandomItemFrom(providers))
                .With(pr => pr.Creator = pr.Order.Creator)
                .Build();

            var images = Builder<Image>
                .CreateListOfSize(_dataCount)
                .Build();
            
                Context.Photos.AddOrUpdate(x => x.Id, images.ToArray());
                Context.ProviderCategories.AddOrUpdate(x => x.Id, providerCategories.ToArray());
                Context.OrderCategories.AddOrUpdate(x => x.Id, orderCategories.ToArray());
                Context.SaveChanges();
                Context.Orders.AddOrUpdate(x => x.Id, orders.ToArray());
                Context.Providers.AddOrUpdate(x => x.Id, providers.ToArray());
                Context.SaveChanges();
                Context.OrderResponses.AddOrUpdate(x => x.Id, orderResponses.ToArray());
                Context.ProviderResponses.AddOrUpdate(x => x.Id, providerResponses.ToArray());
                Context.SaveChanges();
        }

        public OrderResponse CreateOrderResponse(Order order)
        {
            var provider = ProviderRepository.GetBy(x => x.Creator.Id != order.Creator.Id).First();
            return new OrderResponse
            {
                Comment = "Comment",
                Completed = false,
                CreatedAt = DateTime.Now,
                Order = order,
                Provider = provider,
                Creator = provider.Creator
            };
        }
    }
}
