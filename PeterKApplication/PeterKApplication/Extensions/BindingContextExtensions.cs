using System;

namespace PeterKApplication.Extensions
{
    public static class BindingContextExtensions
    {
        public static T As<T>(this object bc)
        {
            if (bc is T t)
            {
                return t;
            }

            throw new Exception("Passed object is not of correct type: " + typeof(T));
        }
    }
}