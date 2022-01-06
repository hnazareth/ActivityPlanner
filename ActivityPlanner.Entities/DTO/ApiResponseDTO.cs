using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ActivityPlanner.Entities.DTO
{
    public class ApiResponseDTO
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public string Message { get; set; }
        public Object Data { get; set; }
    }
}
