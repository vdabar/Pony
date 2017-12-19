using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pony.Framework.Exceptions
{
    public class ApiError
    {
        public string message { get; set; }
        public bool isError { get; set; }
        public string detail { get; set; }

        public ApiError(string message)
        {
            this.message = message;
            isError = true;
        }

        public ApiError()
        {
        }
    }
}