﻿using System.Collections.Generic;
using ServicePlace.Model.LogicModels;

namespace ServicePlace.DataProvider.Interfaces
{
    public interface IExecutorRepository : IRepository<Executor>
    {
        IEnumerable<Executor> Search(string search);

        IEnumerable<Executor> Take(int skip, int count);
    }
}