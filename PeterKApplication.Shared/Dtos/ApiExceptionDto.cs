using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PeterKApplication.Shared.Dtos
{
    public partial class ApiExceptionDto
    {
        public Dictionary<string, string[]> Errors { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public string TraceId { get; set; }
    }
}