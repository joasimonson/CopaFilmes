using System;
using System.Runtime.Serialization;

namespace CopaFilmes.Api
{
    [Serializable]
    class ChaveNaoMontadaException : Exception
    {
        public ChaveNaoMontadaException()
        {
        }

        public ChaveNaoMontadaException(string message) : base(message)
        {
        }

        public ChaveNaoMontadaException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ChaveNaoMontadaException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}