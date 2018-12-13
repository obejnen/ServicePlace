using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using Moq;
using Faker;
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
        private readonly ApplicationContext _context;
        private Random _random;
        private readonly IdentityRepository _identityRepository;
        private readonly CommitProvider _commitProvider;
        private readonly int _dataCount;

        public ProviderRepository ProviderRepository => new ProviderRepository(_context);
        public OrderRepository OrderRepository => new OrderRepository(_context);
        public IdentityRepository IdentityRepository => _identityRepository;
        public ImageRepository ImageRepository => new ImageRepository(_context);
        public OrderResponseRepository OrderResponseRepository => new OrderResponseRepository(_context);
        public ProviderResponseRepository ProviderResponseRepository => new ProviderResponseRepository(_context);
        public OrderCategoryRepository OrderCategoryRepository => new OrderCategoryRepository(_context);
        public ProviderCategoryRepository ProviderCategoryRepository => new ProviderCategoryRepository(_context);
        public CommitProvider CommitProvider => new CommitProvider(_context);

        public ProfileRepository ProfileRepository => new ProfileRepository(_context);
        
        public OrderService OrderService => new OrderService(
            OrderRepository,
            OrderResponseRepository,
            OrderCategoryRepository,
            _commitProvider);

        public ProviderService ProviderService => new ProviderService(
            ProviderRepository,
            ProviderResponseRepository,
            ProviderCategoryRepository,
            _commitProvider
            );

        public UserService UserService => new UserService(
            _identityRepository,
            ProfileRepository,
            _commitProvider);

        public ImageService ImageService => new ImageService();

        public Initializer(int dataCount)
        {
            _dataCount = dataCount;
            _context = new ApplicationContext();
            var userManager = new UserManager(new UserStore(_context));
            var roleManager = new RoleManager(new RoleStore(_context));
            _random = new Random();
            _identityRepository = new IdentityRepository(userManager, roleManager, _context);
        }

        public void ClearDb()
        {
            var connection = new SqlConnection(
                "Data Source=obejnenPC;" +
                "Initial Catalog=ServicePlaceDb;" +
                "Integrated Security=True;" +
                "Connect Timeout=30;" +
                "Encrypt=False;" +
                "TrustServerCertificate=False;" +
                "ApplicationIntent=ReadWrite;" +
                "MultiSubnetFailover=False"
                );
            var cmd = new SqlCommand("DeleteAll", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            connection.Open();
            cmd.ExecuteNonQuery();
        }

        public void InitializeDb()
        {
            _identityRepository.CreateRole(new Role()
            {
                Name = Constants.UserRoleName
            });

            _identityRepository.CreateRole(new Role()
            {
                Name = Constants.AdminRoleName
            });

            var users = Builder<User>
                .CreateListOfSize(_dataCount)
                .All()
                .Build();

            foreach (var user in users)
            {
                _identityRepository.Create(user);
            }
            _context.SaveChanges();

            users = _identityRepository.GetAll().ToList();


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
                .Build();

            var providers = Builder<Provider>
                .CreateListOfSize(_dataCount)
                .All()
                .With(p => p.Creator = Pick<User>.RandomItemFrom(users))
                .With(p => p.Category = Pick<ProviderCategory>.RandomItemFrom(providerCategories))
                .Build();

            var orderResponses = Builder<OrderResponse>
                .CreateListOfSize(_dataCount)
                .All()
                .With(or => or.Provider = Pick<Provider>.RandomItemFrom(providers))
                .With(or => or.Order = Pick<Order>.RandomItemFrom(orders))
                .With(or => or.Creator = or.Provider.Creator)
                .Build();

            var providerResponses = Builder<ProviderResponse>
                .CreateListOfSize(_dataCount)
                .All()
                .With(pr => pr.Order = Pick<Order>.RandomItemFrom(orders))
                .With(pr => pr.Provider = Pick<Provider>.RandomItemFrom(providers))
                .With(pr => pr.Creator = pr.Order.Creator)
                .Build();
            
                _context.ProviderCategories.AddOrUpdate(x => x.Id, providerCategories.ToArray());
                _context.OrderCategories.AddOrUpdate(x => x.Id, orderCategories.ToArray());
                _context.SaveChanges();
                _context.Orders.AddOrUpdate(x => x.Id, orders.ToArray());
                _context.Providers.AddOrUpdate(x => x.Id, providers.ToArray());
                _context.SaveChanges();
                _context.OrderResponses.AddOrUpdate(x => x.Id, orderResponses.ToArray());
                _context.ProviderResponses.AddOrUpdate(x => x.Id, providerResponses.ToArray());
                _context.SaveChanges();
        }
    }
}
