using System;

namespace FakeUp.Exceptions
{
    public class CannotLocateStateException : Exception
    {
        private static string GetMessage(Type type, string tag)
        {
            if (string.IsNullOrEmpty(tag))
            {
                return $"Cannot locate state {type}.";
            }
            return $"Cannot locate state {type} with tag '{tag}'.";
        }
        
        public CannotLocateStateException(Type type, string tag) : base(GetMessage(type, tag)) { }
    }
}