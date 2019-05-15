using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTimeOff.ErrorHandlers
{
    public class NullIdCustomException : Exception
    {
        public NullIdCustomException()
        {

        }

        public NullIdCustomException(string message):base(message)
        {

        }
    }
}
