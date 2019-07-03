using System;

namespace FakeUp.Exceptions
{
    [Serializable]
    public class FillingException : Exception
    {
        public FillingException() { }
        public FillingException(string invocationPath, Type type) : base($"Cannot fill member '{invocationPath}' of type '{type.FullName}'.") { }
        
        public FillingException(string message, Exception inner) : base(message, inner) { }
        protected FillingException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}