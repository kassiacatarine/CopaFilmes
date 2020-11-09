using System;
using System.Collections.Generic;
using System.Text;

namespace Championship.Domain.SeedWork
{
    public abstract class ResponseBase
    {
        public bool Success { get; protected set; }
        public string Message { get; protected set; }
    }

    public class Response<T> : ResponseBase where T : class
    {
        public T Data { get; protected set; }
        public Response(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public Response(bool success, string message, T data) : this(success, message)
        {
            Data = data;
        }
    }
}
