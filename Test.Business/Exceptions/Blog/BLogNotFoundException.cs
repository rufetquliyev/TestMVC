using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Business.Exceptions.Blog
{
    public class BLogNotFoundException : Exception
    {
        public BLogNotFoundException():base("The blog not found.")
        {
        }
        public BLogNotFoundException(string? message) : base(message)
        {
        }
    }
}
