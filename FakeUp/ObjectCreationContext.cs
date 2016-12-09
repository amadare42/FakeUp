using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FakeUp.Config;
using FakeUp.ValueEvaluation;

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
                    new ArrayEvaluator()
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

        public object NewObject(Type type)
        {
            return FakeUp.NewObject(type, this);
        }

        public T RootObject { get; set; }

        object IObjectCreationContext.RootObject
        {
            get { return this.RootObject; }
            set { this.RootObject = (T)value; }
        }

        IInternalFakeUpConfig IObjectCreationContext.Config => this.Config;

        public FakeUpConfig<T> Config { get; set; }

        public string InvocationPath
        {
            get { return string.Join(".", this.InvocationStack.Reverse().Select(propInfo => propInfo.Name).ToArray()); }
        }
    }
}