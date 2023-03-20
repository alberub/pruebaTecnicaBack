using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PruebaTecnica.Services.Exceptions
{
    [Serializable]
    public class PruebaException : Exception
    {
        public int StatusCode { get; set; }

        public PruebaException(string message, int statusCode = 500)
            : base(message)
        {
            StatusCode = statusCode;
        }

        public PruebaException(Exception ex, int statusCode = 500)
            : base(ex.Message)
        {
            StatusCode = statusCode;
        }

        protected PruebaException(SerializationInfo info, StreamingContext context)
        : base(info, context)
        { }
    }
}
