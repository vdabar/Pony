﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pony.Framework.Domain
{
    public interface IRules<T> where T : IAggregateRoot
    {
    }
}
