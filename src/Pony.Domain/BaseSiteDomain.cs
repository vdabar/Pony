﻿using Newtonsoft.Json;
using Pony.Framework.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pony.Domain
{
    public class BaseDomain : ICommand
    {
        [JsonIgnore]
        public Guid Id { get; set; }
    }
}