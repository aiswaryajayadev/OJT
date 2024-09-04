using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class APIResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public static APIResponse SuccessMessage(string message) => new APIResponse { Success = true, Message = message };
        public static APIResponse Failure(string message) => new APIResponse { Success = false, Message = message };

    }
}
