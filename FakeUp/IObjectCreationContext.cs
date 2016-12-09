using System;
using System.Collections.Generic;
using System.Reflection;
using FakeUp.Config;
using FakeUp.ValueEvaluation;

namespace FakeUp
{
    internal interface IObjectCreationContext
    {
        IInternalFakeUpConfig Config { get; }

        // TODO: remove
        object RootObject { get; set; }

        string InvocationPath { get; }

        Stack<PropertyInfo> InvocationStack { get; }

        IValueEvaluator[] Evaluators { get; }

        object NewObject(Type type);
    }
}