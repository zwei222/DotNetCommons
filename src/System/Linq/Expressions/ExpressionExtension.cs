using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using DotNetCommons.System.Linq.Expressions.Internal;

namespace DotNetCommons.System.Linq.Expressions
{
    public static class ExpressionExtension
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> self,
            Expression<Func<T, bool>> expression)
        {
            if (self == null)
            {
                throw new NullReferenceException();
            }

            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            var body = self.Body;
            var parameter = self.Parameters.First();
            var visitor = new ParameterReplaceVisitor(expression.Parameters.First(), parameter);

            body = Expression.AndAlso(body, visitor.Visit(expression.Body));

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> self, Expression<Func<T, bool>> expression)
        {
            if (self == null)
            {
                throw new NullReferenceException();
            }

            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            var body = self.Body;
            var parameter = self.Parameters.First();
            var visitor = new ParameterReplaceVisitor(expression.Parameters.First(), parameter);

            body = Expression.OrElse(body, visitor.Visit(expression.Body));

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }
}
