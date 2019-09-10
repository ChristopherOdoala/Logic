using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic.Model
{
    public class ApiResponse : BaseViewModel
    {
        public ApiResponseCodes Code { get; set; }
        public int Result
        {
            get
            {
                return (int)Code;
            }
        }
        public string Description { get; set; }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T Payload { get; set; } = default(T);
        public int TotalCount { get; set; }
        public string ResponseCode { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public bool HasErrors => Errors.Any();
    }
}
