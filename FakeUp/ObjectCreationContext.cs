using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Playground.ObjectFaker
{
    internal abstract class ObjectCreationContext
    {
        protected ObjectCreationContext()
        {
            InvocationStack = new Stack<PropertyInfo>();
        }

        public Stack<PropertyInfo> InvocationStack { get; set; }
        internal Dictionary<string, Func<object>> RelativeMemberFactories { get; set; } = new Dictionary<string, Func<object>>();
        internal Dictionary<string, Func<object>> AbsoluteMemberFactories { get; set; } = new Dictionary<string, Func<object>>();
        public abstract object RootObject { get; }
        public abstract FakeUpOptions Options { get; }

        public string InvocationPath
        {
            get { return string.Join(".", InvocationStack.Reverse().Select(propInfo => propInfo.Name).ToArray()); }
        }
    }

    internal class ObjectCreationContext<T> : ObjectCreationContext //where T : class //TODO: add class constraint
    {
        public ObjectCreationContext()
        {
            TypedOptions = new FakeUpOptions<T>();
        }

        public FakeUpOptions<T> TypedOptions { get; set; }

        public override object RootObject => TypedRootObject;

        public override FakeUpOptions Options => TypedOptions;

        public T TypedRootObject { get; set; }
    }
}