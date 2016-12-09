using System;
using System.Collections.Generic;
using System.Reflection;

namespace FakeUp
{
    internal interface IObjectCreationContext
    {
        IInternalFakeUpConfig Config { get; }

        object RootObject { get; set; }

        string InvocationPath { get; }

        Stack<PropertyInfo> InvocationStack { get; }

        IValueEvaluator[] Evaluators { get; }

        object NewObject(Type type);
    }
}