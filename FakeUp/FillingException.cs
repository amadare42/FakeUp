using System;

namespace FakeUp
{
    [Serializable]
    public class FillingException : Exception
    {
        public FillingException() { }
        public FillingException(string message) : base(message) { }
        public FillingException(string message, Exception inner) : base(message, inner) { }
        protected FillingException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}