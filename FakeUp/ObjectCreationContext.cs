using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FakeUp.Config;
using FakeUp.RelativePathing;
using FakeUp.States;
using FakeUp.ValueEvaluation;
using FakeUp.ValueEvaluation.Evaluators;

namespace FakeUp
{
    internal class ObjectCreationContext<TFakeObject> : IObjectCreationContext
    {
        private readonly IValueEvaluator[] DefaultValueEvaluators = {
            new AbsolutePathEvaluator(),
            new RelativePathEvaluator(),
            new TypeEvaluator(),
            new EmptyStringEvaluator(),
            new ListEvaluator(),
            new ArrayEvaluator(),
            new ActivatorEvaluator()
        };

        internal ObjectCreationContext(FakeUpConfig<TFakeObject> config)
        {
            this.Config = config;
            this.StatesRepository = this.Config.StatesConfig.GetRepository();
            this.InvocationStack = new Stack<PropertyInfo>();
            this.Evaluators = config.ValueEvaluators.Concat(DefaultValueEvaluators).ToArray();
        }

        public Stack<PropertyInfo> InvocationStack { get; set; }
        
        public StatesRepository StatesRepository { get; }

        public IValueEvaluator[] Evaluators { get; }

        public T GetState<T>(string tag = "")
        {
            return this.StatesRepository.GetState<T>(tag);
        }

        public Type CurrentPropertyType => this.InvocationStack.Any() ? this.InvocationStack.Peek().PropertyType : null;

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

        public FakeUpConfig<TFakeObject> Config { get; set; }

        public string InvocationPath
        {
            get { return string.Join(".", this.InvocationStack.Reverse().Select(propInfo => propInfo.Name).ToArray()); }
        }
    }
}