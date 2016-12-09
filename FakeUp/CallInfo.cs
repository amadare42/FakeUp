using System;
using System.Collections.Generic;
using System.Reflection;

namespace FakeUp
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
            return (this.PropType == propertyInfo.PropertyType) && (this.PropName == propertyInfo.Name);
        }
    }
}