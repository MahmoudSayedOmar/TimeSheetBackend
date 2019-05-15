using System;

namespace EmployeeTimeOff.Controllers
{
    internal class NotFoundCustomException : Exception
    {

        public NotFoundCustomException()
        {
        }

        public NotFoundCustomException(string message) : base(message)
        {
        }

        

        public NotFoundCustomException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}