using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWW12.Exceptions
{
    public class ReviewNotFoundException : ValidationException
    {
        public ReviewNotFoundException(string message) : base(message)
        {
        }
    }
}
