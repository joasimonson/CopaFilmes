using System;
using System.Runtime.Serialization;

namespace CopaFilmes.Api
{
    [Serializable]
    public class RegraException : Exception
    {
        public RegraException()
        {
        }

        public RegraException(string message) : base(message)
        {
        }

        public RegraException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RegraException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}