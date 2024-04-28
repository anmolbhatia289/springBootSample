using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharing.Exceptions
{
    public class InvalidSplitException : Exception
    {
        public InvalidSplitException(string message) : base(message) { }
    }
}
