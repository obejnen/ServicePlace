﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ServicePlace.Model;

namespace ServicePlace.DataProvider.Interfaces
{
    public interface IUsersRolesRepository
    {
        Task AddToRoleAsync(User user, string roleId);

        Task RemoveFromRoleAsync(User user, string roleId);

        Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken);

        void Dispose();
    }
}