using System;

namespace FakeUp.Exceptions
{
    public class StateAlreadyPresentException : Exception
    {
        private static string GetMessage(Type type, string tag)
        {
            if (string.IsNullOrEmpty(tag))
            {
                return $"Cannot add state {type}: it is already present.";
            }
            return $"Cannot add state {type} with tag '{tag}': it is already present.";
        }
        
        public StateAlreadyPresentException(Type type, string tag) : base(GetMessage(type, tag)) { }
    }
}