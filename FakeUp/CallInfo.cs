using System;
using System.Reflection;

namespace FakeUpLib
{
    public class CallInfo
    {

        public CallInfo()
        {
        }

        public CallInfo(PropertyInfo propertyInfo)
        {
            this.PropertyInfo = propertyInfo;
        }
        public PropertyInfo PropertyInfo { get; }

        public Type PropType => this.PropertyInfo.PropertyType;
        public string PropName => this.PropertyInfo.Name;

        public bool IsSameCall(PropertyInfo propertyInfo)
        {
            return this.PropertyInfo == propertyInfo;
        }

        public override string ToString()
        {
            return $"CallInfo[{this.PropertyInfo}]";
        }
    }
}