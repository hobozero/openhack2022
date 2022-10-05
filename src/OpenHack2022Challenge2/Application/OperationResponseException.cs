using System;
using System.Collections.Generic;
using System.Text;

namespace OpenHack2022.Application
{

    [Serializable]
    public class OperationResponseException : Exception
    {
        public object ErrorData { get; set; }

        public OperationResponseException() { }
        public OperationResponseException(string message) : base(message) { }
        public OperationResponseException(string message, Exception inner) : base(message, inner) { }
        protected OperationResponseException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}   
