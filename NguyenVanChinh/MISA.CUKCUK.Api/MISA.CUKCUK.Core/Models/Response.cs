using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Models
{
    public class Response
    {
        public Response(dynamic data, bool success, string errorrMess)
        {
            Data = data;
            Success = success;
            ErrorrMess = errorrMess;
        }

        public dynamic Data { get; set; }
        public bool Success { get; set; }
        public string ErrorrMess { get; set; }
    }
}
