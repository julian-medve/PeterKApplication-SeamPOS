using System;

namespace PeterKApplication.Web.Exceptions
{
    public class AppException : Exception
    {
        public AppException() : base() {}
        public AppException(string message) : base(message) {}
    }
}