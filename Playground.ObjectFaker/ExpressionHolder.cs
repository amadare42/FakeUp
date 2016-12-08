using System;
using System.Linq.Expressions;

namespace Playground.ObjectFaker
{
    public class ExpressionHolder<TFakeObject, TMember> : ExpressionHolder
    {
        public readonly Expression<Func<TFakeObject, TMember>> Expr;
        public readonly Func<TFakeObject, TMember> Compiled;


        public ExpressionHolder(Expression<Func<TFakeObject, TMember>> expr, string path) 
            : base(typeof(TFakeObject), path)
        {
            this.Expr = expr;
            Compiled = expr.Compile();
        }

        public object GetValue(TFakeObject obj)
        {
            return Compiled(obj);
        }

        public override object GetValue(object obj)
        {
            if (!(obj is TFakeObject))
            {
                throw new Exception("Wrong type");
            }
            var fake = (TFakeObject) obj;
            return Compiled(fake);
        }
    }

    public abstract class ExpressionHolder
    {
        public Type RequestedType { get; }

        public readonly string InvokationPath;

        protected ExpressionHolder(Type requestedType, string path)
        {
            RequestedType = requestedType;
        }

        public abstract object GetValue(object obj);
    }
}