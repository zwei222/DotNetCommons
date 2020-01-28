// ---------------------------------------------------------------------
// <copyright file="ExpressionExtension.cs" company="zwei222">
// Copyright (c) zwei222. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Expressions;
using DotNetCommons.System.Linq.Expressions.Internal;

namespace DotNetCommons.System.Linq.Expressions
{
    /// <summary>
    /// Class that extends Expression class.
    /// </summary>
    public static class ExpressionExtension
    {
        /// <summary>
        /// Joins expression trees with logical conjunction.
        /// </summary>
        /// <typeparam name="T">Type parameter.</typeparam>
        /// <param name="self">Myself</param>
        /// <param name="expression">Expression tree to join.</param>
        /// <returns>Combined expression tree.</returns>
        public static Expression<Func<T, bool>> And<T>(
            this Expression<Func<T, bool>> self,
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
            var visitor = new ReplaceParameterVisitor(expression.Parameters.First(), parameter);

            body = Expression.AndAlso(body, visitor.Visit(expression.Body));

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }

        /// <summary>
        /// Joins expression trees by logical OR.
        /// </summary>
        /// <typeparam name="T">Type parameter.</typeparam>
        /// <param name="self">Myself</param>
        /// <param name="expression">Expression tree to join.</param>
        /// <returns>Combined expression tree.</returns>
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
            var visitor = new ReplaceParameterVisitor(expression.Parameters.First(), parameter);

            body = Expression.OrElse(body, visitor.Visit(expression.Body));

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }
}
