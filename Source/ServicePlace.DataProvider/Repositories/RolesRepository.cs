using AutoMapper;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using ServicePlace.DataProvider.DbContexts;
using ServicePlace.DataProvider.Interfaces;
using DataModels = ServicePlace.DataProvider.Models;
using CommonModels = ServicePlace.Model;

namespace ServicePlace.DataProvider.Repositories
{
    public class RolesRepository : IRolesRepository
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public RolesRepository(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<IdentityResult> CreateAsync(CommonModels.Role role, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<DataModels.Role>(role);
            _context.Add(model);
            return Task.FromResult(_context.SaveChangesAsync(cancellationToken).Result > 0
                ? IdentityResult.Success
                : IdentityResult.Failed(new IdentityError
                {
                    Code = string.Empty,
                    Description = $"The role with name {role.Name} could not be inserted in the dbo.Roles table."
                }));
        }

        public Task<IdentityResult> DeleteAsync(CommonModels.Role role, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<DataModels.Role>(role);
            _context.Remove(model);
            return Task.FromResult(_context.SaveChangesAsync(cancellationToken).Result > 0
                ? IdentityResult.Success
                : IdentityResult.Failed(new IdentityError
                {
                    Code = string.Empty,
                    Description = $"The role with name {role.Name} could not be updated in the dbo.Roles table."

                }));
        }

        public Task<IdentityResult> UpdateAsync(CommonModels.Role role, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<DataModels.Role>(role);
            DataModels.Role roleToEdit = _context.Roles.SingleOrDefault(x => x.Id == role.Id);
            roleToEdit = model;
            return Task.FromResult(_context.SaveChangesAsync(cancellationToken).Result > 0
                ? IdentityResult.Success
                : IdentityResult.Failed(new IdentityError
                {
                    Code = string.Empty,
                    Description = $"The role with name {role.Name} could not be deleted from the dbo.Roles table."
                }));
        }

        public Task<CommonModels.Role> FindByIdAsync(string id)
        {
            var model = _context.Roles.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(_mapper.Map<CommonModels.Role>(model));
        }

        public Task<CommonModels.Role> FindByNameAsync(string name)
        {
            var model = _context.Roles.FirstOrDefault(x => x.Name == name);
            return Task.FromResult(_mapper.Map<CommonModels.Role>(model));
        }

        public Task<IEnumerable<CommonModels.Role>> GetAll()
        {
            return Task.FromResult(_mapper.Map<IEnumerable<CommonModels.Role>>(_context.Roles));
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}