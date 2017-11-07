﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pony.Framework.Queries
{
    public interface IQueryHandlerAsync<in TQuery, TResult> where TQuery : IQuery
    {
        Task<TResult> RetrieveAsync(TQuery query);
    }
}
