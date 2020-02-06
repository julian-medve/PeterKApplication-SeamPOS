using System;
using System.Linq;
using System.Linq.Expressions;
using PeterKApplication.Shared.Dtos;
using Refit;
using Xamarin.Forms.Internals;

namespace PeterKApplication.Models
{
    public class ApiExecutionResponse<T>
    {
        public bool HasGeneralError => Error?.Errors?.ContainsKey(string.Empty) == true;
        public bool HasError => Error != null;
        public T Response { get; set; }
        public ProblemDetails Error { get; set; }

        public string GeneralError => Error?.Errors?.ContainsKey(string.Empty) == true ? Error.Errors[string.Empty].First() : null;
    }

    public class ApiExecutionResponse
    {
        public bool HasGeneralError => Error?.Errors?.ContainsKey(string.Empty) == true;
        public bool HasError => Error != null;
        public ProblemDetails Error { get; set; }

        public string GeneralError => Error?.Errors?.ContainsKey(string.Empty) == true ? Error.Errors[string.Empty].First() : null;
    }
}