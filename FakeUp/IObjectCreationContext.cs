using System;
using System.Reflection;
using FakeUpLib.Config;
using FakeUpLib.RelativePathing;
using FakeUpLib.ValueEvaluation;

namespace FakeUpLib
{
    public interface IObjectCreationContext
    {
        IInternalFakeUpConfig Config { get; }

        string InvocationPath { get; }

        IValueEvaluator[] Evaluators { get; }

        T GetState<T>(string tag = "");

        Type CurrentPropertyType { get; }

        object NewObject(Type type);

        void PushInvocation(PropertyInfo propertyInfo);

        PropertyInfo PopInvocation();

        int GetMatchScore(BaseRelativeMemberInfo relativeMemberInfo);

        int GetCyclicReferencesDepth();
    }
}