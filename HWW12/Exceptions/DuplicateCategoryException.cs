using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWW12.Exceptions
{
    public class DuplicateCategoryException : ValidationException
    {
        public DuplicateCategoryException(string message) : base(message)
        {
        }
    }
}
