using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Business.Exceptions.Common
{
    public class NegativeIdException : Exception
    {
        public NegativeIdException():base("Invalid ID received.")
        {

        }
        public NegativeIdException(string? message) : base(message)
        {
        }
    }
}
