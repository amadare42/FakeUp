using System;
using System.Reflection;

namespace Playground.ObjectFaker
{
    public class CallInfo
    {
        public CallInfo()
        {
        }

        public CallInfo(Type propType, string propName)
        {
            this.PropType = propType;
            this.PropName = propName;
        }

        public Type PropType { get; set; }
        public string PropName { get; set; }

        public bool IsSameCall(PropertyInfo propertyInfo)
        {
            return PropType == propertyInfo.PropertyType && PropName == propertyInfo.Name;
        }
    }
}