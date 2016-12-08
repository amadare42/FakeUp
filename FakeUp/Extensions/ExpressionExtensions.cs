using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Playground.ObjectFaker
{
    internal static class ExpressionExtensions
    {
        internal static string ToCallPath<TObject, TMember>(this Expression<Func<TObject, TMember>> expr)
        {
            var stack = new Stack<string>();

            MemberExpression me;
            switch (expr.Body.NodeType)
            {
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                    var ue = expr.Body as UnaryExpression;
                    me = ((ue != null) ? ue.Operand : null) as MemberExpression;
                    break;

                default:
                    me = expr.Body as MemberExpression;
                    break;
            }


            while (me != null)
            {
                stack.Push(me.Member.Name);
                me = me.Expression as MemberExpression;
            }

            return string.Join(".", stack.ToArray());
        }

        internal static CallChain ToCallChain<TObject, TMember>(this Expression<Func<TObject, TMember>> expr)
        {
            return new CallChain(expr.SplitToCalls().ToList());
        }

        internal static IEnumerable<CallInfo> SplitToCalls<TObject, TMember>(this Expression<Func<TObject, TMember>> expr)
        {
            var stack = new Stack<CallInfo>();

            MemberExpression me;
            switch (expr.Body.NodeType)
            {
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                    var ue = expr.Body as UnaryExpression;
                    me = ((ue != null) ? ue.Operand : null) as MemberExpression;
                    break;

                default:
                    me = expr.Body as MemberExpression;
                    break;
            }


            while (me != null)
            {
                var call = new CallInfo()
                {
                    PropName = me.Member.Name,
                    PropType = ((PropertyInfo)me.Member).PropertyType
                };
                stack.Push(call);
                me = me.Expression as MemberExpression;
            }

            return stack.ToList();
        }
    }
}