using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ServicePlace.DataProvider.DbContexts;
using CommonModels = ServicePlace.Model;
using DataModels = ServicePlace.DataProvider.Models;
using ServicePlace.DataProvider.Interfaces;

namespace DL.Repositories
{
    public class UsersRolesRepository : IUsersRolesRepository
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public UsersRolesRepository(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task AddToRoleAsync(CommonModels.User user, string roleId)
        {
            var model = new DataModels.UserRole
            {
                RoleId = roleId,
                UserId = user.Id
            };

            _context.Add(model);
            return Task.FromResult(_context.SaveChangesAsync());
        }

        public Task RemoveFromRoleAsync(CommonModels.User user, string roleId)
        {
            var model = _context.UserRoles.FirstOrDefault(x => x.UserId == user.Id && x.RoleId == roleId);
            _context.UserRoles.Remove(model ?? throw new InvalidOperationException());
            return Task.FromResult(_context.SaveChangesAsync());
        }

        public Task<IList<string>> GetRolesAsync(CommonModels.User user, CancellationToken cancellationToken)
        {
            IEnumerable<DataModels.Role> result = _context.UserRoles.Where(x => x.UserId == user.Id).Select(x => x.Role);
            var userRoles = _mapper.Map<IEnumerable<CommonModels.Role>>(result);

            return Task.FromResult<IList<string>>(userRoles.Select(x => x.Name).ToList());
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}