using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Business.Exceptions.User
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base("The user not found.")
        {
        }
        public UserNotFoundException(string? message) : base(message)
        {
        }
    }
}
