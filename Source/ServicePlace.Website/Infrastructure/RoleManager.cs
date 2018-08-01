using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ServicePlace.Model;

namespace ServicePlace.Website.Infrastructure
{
    public class RoleManager : RoleManager<Role>
    {
        public RoleManager(IRoleStore<Role> store,
            IEnumerable<IRoleValidator<Role>> roleValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            ILogger<RoleManager<Role>> logger)
            : base(store, roleValidators, keyNormalizer, errors, logger)
        {
        }
    }
}
