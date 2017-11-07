using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pony.Framework.Events
{
    public interface IEvent
    {
        DateTime TimeStamp { get; set; }
        Guid UserId { get; set; }
    }
}
