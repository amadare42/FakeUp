using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FakeUp.Config;
using FakeUp.RelativePathing;
using FakeUp.ValueEvaluation;
using FakeUp.ValueEvaluation.Evaluators;

namespace FakeUp
{
    internal class ObjectCreationContext<T> : IObjectCreationContext
    {
        private static IValueEvaluator[] DefaultValueEvaluators
        {
            get
            {
                return new IValueEvaluator[]
                {
                    new AbsolutePathEvaluator(),
                    new RelativePathEvaluator(),
                    new TypeEvaluator(),
                    new EmptyStringEvaluator(),
                    new ListEvaluator(),
                    new ArrayEvaluator(),
                    new ActivatorEvaluator()
                };
            }
        }

        internal ObjectCreationContext(FakeUpConfig<T> config)
        {
            this.Config = config;
            this.InvocationStack = new Stack<PropertyInfo>();
            this.Evaluators = DefaultValueEvaluators;
        }

        public Stack<PropertyInfo> InvocationStack { get; set; }

        public IValueEvaluator[] Evaluators { get; }

        public Type CurrentPropertyType
        {
            get
            {
                return this.InvocationStack.Peek().PropertyType;
            }
        }

        public object NewObject(Type type)
        {
            return FakeUp.NewObject(type, this);
        }

        public void PushInvocation(PropertyInfo propertyInfo)
        {
            this.InvocationStack.Push(propertyInfo);
        }

        public PropertyInfo PopInvocation()
        {
            return this.InvocationStack.Pop();
        }

        public int GetMatchScore(CallChain callChain)
        {
            return callChain.GetMatchScore(this.InvocationStack);
        }

        public int GetCyclicReferencesDepth()
        {
            var currentType = this.InvocationStack.Peek().PropertyType;
            return this.InvocationStack.Count(propInfo => propInfo.PropertyType == currentType);
        }

        IInternalFakeUpConfig IObjectCreationContext.Config => this.Config;

        public FakeUpConfig<T> Config { get; set; }

        public string InvocationPath
        {
            get { return string.Join(".", this.InvocationStack.Reverse().Select(propInfo => propInfo.Name).ToArray()); }
        }
    }
}