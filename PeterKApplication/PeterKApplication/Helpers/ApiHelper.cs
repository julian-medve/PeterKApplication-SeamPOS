using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PeterKApplication.Models;
using PeterKApplication.Shared.Dtos;
using Refit;
using Remotion.Linq.Clauses;

namespace PeterKApplication.Helpers
{
    public class ApiHelper
    {
        public static async Task<ApiExecutionResponse<T>> Execute<T>(Task<T> task) where T : new()
        {
            var res = new ApiExecutionResponse<T>();

            try
            {   
                await task;
                res.Response = task.Result;
            }
            catch (ValidationApiException e)
            {
                res.Error = e.Content;
            }
            catch (ApiException e)
            {
                var errorParsed = JsonConvert.DeserializeObject<ProblemDetails>(e.Content);
                res.Error = errorParsed;
            }
            catch (Exception e)
            {
                res.Error = new ProblemDetails
                {
                    Errors = new Dictionary<string, string[]>
                    {
                        {"", new[] {e.Message}}
                    }
                };
            }

            return res;
        }

        public static async Task<ApiExecutionResponse> Execute(Task task)
        {
            var res = new ApiExecutionResponse();
            
            try
            {
                await task;
            }
            catch (ValidationApiException e)
            {
                res.Error = e.Content;
            }
            catch (ApiException e)
            {
                var errorParsed = JsonConvert.DeserializeObject<ProblemDetails>(e.Content);
                res.Error = errorParsed;
            }
            catch (Exception e)
            {
                res.Error = new ProblemDetails
                {
                    Errors = new Dictionary<string, string[]>
                    {
                        {"", new[] {e.Message}}
                    }
                };
            }

            return res;
        }
    }
}