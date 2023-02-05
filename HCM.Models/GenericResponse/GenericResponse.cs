using HCM.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Models.GenericResponse
{
    public class GenericResponse<T> where T : class
    {
        public int statusCode;
        public T payload;
        public string message;
        public bool status;

        public static GenericResponse<T> Success(T payload, string message, ApiStatusCode statusCode)
        {
            return new GenericResponse<T>()
            {
                payload = payload,
                status = true,
                message = message,
                statusCode = (int)statusCode
            };
        }

        public static GenericResponse<T> Success(string message, ApiStatusCode statusCode)
        {
            return new GenericResponse<T>()
            {
                status = true,
                message = message,
                statusCode = (int)statusCode
            };
        }

        public static GenericResponse<T> Failure(T payload, string message, ApiStatusCode statusCode)
        {
            return new GenericResponse<T>()
            {
                payload = payload,
                status = false,
                message = message,
                statusCode = (int)statusCode
            };
        }

        public static GenericResponse<T> Failure(string message, ApiStatusCode statusCode)
        {
            return new GenericResponse<T>()
            {
                status = false,
                message = message,
                statusCode = (int)statusCode
            };
        }
    }

    
}
