using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FakeUp.RelativePathing;

namespace FakeUp.Extensions
{
    internal static class ExpressionExtensions
    {
        internal static CallChain ToCallChain<TObject, TMember>(this Expression<Func<TObject, TMember>> expr)
        {
            return new CallChain(expr.SplitToCalls().ToList());
        }

        internal static IEnumerable<CallInfo> SplitToCalls<TObject, TMember>(
            this Expression<Func<TObject, TMember>> expr)
        {
            return GetPropertyInfosSequence(expr)
                .Select(member => new CallInfo(member.PropertyType, member.Name));
        }

        internal static string ToCallPath<TObject, TMember>(this Expression<Func<TObject, TMember>> expr)
        {
            var propInfos = GetPropertyInfosSequence(expr);

            return string.Join(".", propInfos.Select(info => info.Name).ToArray());
        }

        private static PropertyInfo[] GetPropertyInfosSequence<TObject, TMember>(Expression<Func<TObject, TMember>> expr)
        {
            var stack = new Stack<PropertyInfo>();

            MemberExpression me;
            switch (expr.Body.NodeType)
            {
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                    var ue = expr.Body as UnaryExpression;
                    me = ue?.Operand as MemberExpression;
                    break;

                default:
                    me = expr.Body as MemberExpression;
                    break;
            }


            while (me != null)
            {
                stack.Push((PropertyInfo)me.Member);
                me = me.Expression as MemberExpression;
            }

            return stack.ToArray();
        }
    }
}