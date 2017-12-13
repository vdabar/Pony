using Pony.Framework.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pony.Domain
{
    public class BaseSiteCommand : ICommand
    {
        public Guid SiteId { get; set; }
    }
}