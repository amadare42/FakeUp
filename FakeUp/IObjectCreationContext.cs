using System;
using System.Reflection;
using FakeUp.Config;
using FakeUp.RelativePathing;
using FakeUp.ValueEvaluation;

namespace FakeUp
{
    internal interface IObjectCreationContext
    {
        IInternalFakeUpConfig Config { get; }

        string InvocationPath { get; }

        IValueEvaluator[] Evaluators { get; }

        Type CurrentPropertyType { get; }

        object NewObject(Type type);

        void PushInvocation(PropertyInfo propertyInfo);

        PropertyInfo PopInvocation();

        int GetMatchScore(CallChain callChain);

        int GetCyclicReferencesDepth();
    }
}